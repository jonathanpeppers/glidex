#load "helpers.cake"

// Input args
string target = Argument("target", "Default");
string configuration = Argument("configuration", "Release");

// Define vars
var dirs = new[] 
{
    Directory("./build"),
    Directory("./glidex/bin") + Directory(configuration),
    Directory("./glidex/obj") + Directory(configuration),
    Directory("./glidex.forms/bin") + Directory(configuration),
    Directory("./glidex.forms/obj") + Directory(configuration),
    Directory("./glidex.sample/bin") + Directory(configuration),
    Directory("./glidex.sample/obj") + Directory(configuration),
    Directory("./glidex.forms.sample/bin") + Directory(configuration),
    Directory("./glidex.forms.sample/obj") + Directory(configuration),
};
string output = dirs[0];
string sln = "./glidex.sln";
string version = "1.0.3";
string suffix = "";

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
        MSBuild(sln, new MSBuildSettings {
            Configuration = configuration,
            ToolPath = msbuild,
        });
    });

Task("Install")
    .IsDependentOn("Build")
    .Does(() =>
    {
        MSBuild("./glidex.forms.sample/glidex.forms.sample.csproj", new MSBuildSettings {
            Configuration = configuration,
            ToolPath = msbuild,
        }.WithTarget("Install").WithTarget("_Run"));
    });

Task("NuGet-Package-GlideX")
    .IsDependentOn("Build")
    .Does(() =>
    {
        package(version + suffix, "./glidex/glidex.nuspec", "glidex.dll", output);
    });

Task("NuGet-Push-GlideX")
    .Does(() =>
    {
        push("./build/glidex." + version + suffix + ".nupkg");
    });

Task("NuGet-Package-GlideX-Forms")
    .IsDependentOn("Build")
    .Does(() =>
    {
        package(version + suffix, "./glidex.forms/glidex.forms.nuspec", "glidex.forms.dll", output);
    });

Task("NuGet-Push-GlideX-Forms")
    .Does(() =>
    {
        push("./build/glidex.forms." + version + suffix + ".nupkg");
    });

Task("NuGet-Package")
    .IsDependentOn("NuGet-Package-GlideX")
    .IsDependentOn("NuGet-Package-GlideX-Forms");

Task("NuGet-Push")
    .IsDependentOn("NuGet-Push-GlideX")
    .IsDependentOn("NuGet-Push-GlideX-Forms");

Task("Default")
    .IsDependentOn("NuGet-Package");

RunTarget(target);
