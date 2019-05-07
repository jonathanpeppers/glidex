#load "helpers.cake"

// Input args
string target = Argument("target", "Default");
string configuration = Argument("configuration", "Release");

// Define vars
var dirs = new[] 
{
    Directory("./build"),
    Directory("./glidex.forms/bin") + Directory(configuration),
    Directory("./glidex.forms/obj") + Directory(configuration),
    Directory("./glidex.forms.sample/bin") + Directory(configuration),
    Directory("./glidex.forms.sample/obj") + Directory(configuration),
};
string output = dirs[0];
string sln = "./glidex.sln";
string version = "2.0.0";
string suffix = "pre1";

Task("Clean")
    .Does(() =>
    {
        foreach (var dir in dirs)
            CleanDirectory(dir);
    });

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        NuGetRestore(sln);
    });

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
    {
        MSBuild(sln, settings => settings.SetConfiguration(configuration));
    });

Task("Install")
    .IsDependentOn("Build")
    .Does(() =>
    {
        MSBuild("./glidex.forms.sample/glidex.forms.sample.csproj", settings => settings.SetConfiguration(configuration).WithTarget("Install").WithTarget("_Run"));
    });

Task("NuGet-Package")
    .IsDependentOn("Build")
    .Does(() =>
    {
        package(version + suffix, "./glidex.forms/glidex.forms.nuspec", "glidex.forms.dll", output);
    });

Task("NuGet-Push")
    .Does(() =>
    {
        push("./build/glidex.forms." + version + suffix + ".nupkg");
    });

Task("Default")
    .IsDependentOn("NuGet-Package");

RunTarget(target);
