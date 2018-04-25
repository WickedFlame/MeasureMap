$root = (split-path -parent $MyInvocation.MyCommand.Definition)
$version = [System.Reflection.Assembly]::LoadFile("$root\src\MeasureMap\bin\Release\MeasureMap.dll").GetName().Version

<#
# Create NuGet package for RC
$versionStr = "{0}.{1}.{2}-RC0{3}" -f ($version.Major, $version.Minor, $version.Build, $version.Revision)

Write-Host "Setting .nuspec version tag to $versionStr"

$content = (Get-Content $root\src\MeasureMap.nuspec) 
$content = $content -replace '\$version\$',$versionStr

$content | Out-File $root\src\MeasureMap.compiled.nuspec

& $root\build\NuGet.exe pack $root\src\MeasureMap.compiled.nuspec
#>

# Create NuGet package for Release
$versionStr = "{0}.{1}.{2}" -f ($version.Major, $version.Minor, $version.Build)

Write-Host "Setting .nuspec version tag to $versionStr"

$content = (Get-Content $root\src\MeasureMap.nuspec) 
$content = $content -replace '\$version\$',$versionStr

$content | Out-File $root\src\MeasureMap.compiled.nuspec

& $root\build\NuGet.exe pack $root\src\MeasureMap.compiled.nuspec