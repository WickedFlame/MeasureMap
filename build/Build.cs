using System;
using System.Linq;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[CheckBuildProjectConfigurations]
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
    public string Version { get; set; } = $"1.2.11";

    [Parameter("The Buildnumber provided by the CI")]
    public string BuildNo = $"{DateTime.Today.Month * 31 + DateTime.Today.Day}0";

    [Parameter("Is RC Version")]
    public bool IsRc = false;

    AbsolutePath SourceDirectory => RootDirectory / "src";

    AbsolutePath TestsDirectory => RootDirectory / "src" / "tests";

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            SourceDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
            TestsDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
        });

    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetVersion(BuildVersion())
                .SetAssemblyVersion(BuildVersion())
                .SetFileVersion(BuildVersion())
                .AddProperty("PackageVersion", PackageVersion())
                .EnableNoRestore());
        });

    Target Test => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetTest(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration));
        });

    Target Release => _ => _
        //.DependsOn(Clean)
        //.DependsOn(Compile)
        .DependsOn(Test);


    string BuildVersion()
        => $"{Version}.{BuildNo}";

    string PackageVersion() 
        => IsRc ? int.Parse(BuildNo) < 10 ? $"{Version}-RC0{BuildNo}" : $"{Version}-RC{BuildNo}" : Version;

}
