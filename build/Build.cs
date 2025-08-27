using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.Coverlet;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.SonarScanner;
using Nuke.Common.Utilities.Collections;
using System.IO;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using static Nuke.Common.Tools.SonarScanner.SonarScannerTasks;

[ShutdownDotNetAfterServerBuild]
class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    //
    // nuke compile --isrc true --version 1.1.1 --buildno 5
    // https://www.appveyor.com/blog/2014/07/23/appveyor-yml-and-multiple-branches/
    //

    public static int Main () => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = Configuration.Release; // IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] readonly Solution Solution;
    [GitRepository] readonly GitRepository GitRepository;

    [Parameter("Version to be injected in the Build")]
    public string Version { get; set; } = $"3.0.0";

    [Parameter("The Buildnumber provided by the CI")]
    public int BuildNo = 16;

    [Parameter("Is RC Version")]
    public bool IsRc = false;

    [Parameter("Run UnitTests on build")]
    public bool RunTests = true;

    AbsolutePath SourceDirectory => RootDirectory / "src";

    AbsolutePath TestsDirectory => RootDirectory / "src" / "tests";

    AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";

    AbsolutePath DeployPath => (AbsolutePath)"C:" / "Projects" / "NuGet Store";

    [Parameter("Full name of the Project. This is defined in parameters.json")]
    readonly string ProjectName;

    [Parameter("URL of the SonarQube Server")]
    public string SonarServer = "https://sonarcloud.io";

    [Parameter("Login Token of the SonarQube Server")]
    public string SonarToken = "";

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            SourceDirectory.GlobDirectories("**/bin", "**/obj").ForEach(d => d.DeleteDirectory());
            TestsDirectory.GlobDirectories("**/bin", "**/obj").ForEach(d => d.DeleteDirectory());
            ArtifactsDirectory.CreateOrCleanDirectory();
        });

    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .DependsOn(Clean, Restore)
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetVersion($"{Version}.{BuildNo}")
                .SetAssemblyVersion($"{Version}.{BuildNo}")
                .SetFileVersion(Version)
                .SetInformationalVersion($"{Version}.{BuildNo}")
                .AddProperty("PackageVersion", PackageVersion)
                .EnableNoRestore());
        });

    Target Test => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            if (!RunTests)
            {
                return;
            }

            DotNetTest(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetNoBuild(true)
                .SetExcludeByFile("MeasureMap.IntegrationTest")
                .EnableNoRestore());
        });

    Target Release => _ => _
        .DependsOn(Test)
        .Executes(() =>
        {
            // copy to artifacts folder
            foreach (var file in Directory.GetFiles(RootDirectory, $"*.{PackageVersion}.nupkg", SearchOption.AllDirectories))
            {
                ((AbsolutePath) file).CopyToDirectory(ArtifactsDirectory, ExistsPolicy.FileOverwrite);
            }

            foreach (var file in Directory.GetFiles(RootDirectory, $"*.{PackageVersion}.snupkg", SearchOption.AllDirectories))
            {
                ((AbsolutePath) file).CopyToDirectory(ArtifactsDirectory, ExistsPolicy.FileOverwrite);
            }
        });

    Target Deploy => _ => _
        .DependsOn(Release)
        .Executes(() =>
        {
            // copy to local store
            foreach (var file in Directory.GetFiles(ArtifactsDirectory, $"*.{PackageVersion}.nupkg", SearchOption.AllDirectories))
            {
                ((AbsolutePath) file).CopyToDirectory(DeployPath, ExistsPolicy.FileOverwrite);
            }

            foreach (var file in Directory.GetFiles(ArtifactsDirectory, $"*.{PackageVersion}.snupkg", SearchOption.AllDirectories))
            {
                ((AbsolutePath) file).CopyToDirectory(DeployPath, ExistsPolicy.FileOverwrite);
            }
        });

    Target Sonar => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            Serilog.Log.Write(Serilog.Events.LogEventLevel.Information, $"start sonar for {ProjectName}");

            SonarScannerBegin(s => s
                .SetProjectKey("WickedFlame_MeasureMap")
                .SetName(ProjectName)
                .SetOrganization("wickedflame")
                .SetFramework("net8.0")
                .SetServer(SonarServer)
                .SetLogin(SonarToken)
                .SetOpenCoverPaths("Tests/**/coverage.opencover.xml"));

            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration.Debug)
                .EnableNoRestore());

            DotNetTest(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration.Debug)
                .SetNoBuild(true)
                .EnableNoRestore()
                .EnableCollectCoverage()
                //.SetFramework("net8.0")
                .SetCoverletOutputFormat(CoverletOutputFormat.opencover));

            Serilog.Log.Write(Serilog.Events.LogEventLevel.Information, "end sonar");

            SonarScannerEnd(s => s
                .SetFramework("net8.0")
                .SetLogin(SonarToken));
        });

    string PackageVersion 
        => IsRc ? BuildNo < 10 ? $"{Version}-RC0{BuildNo}" : $"{Version}-RC{BuildNo}" : Version;

}
