@echo off

echo Begin create nuget for MeasureMap
nuget.exe pack ..\src\MeasureMap.nuspec

echo End create nuget for MeasureMap
pause