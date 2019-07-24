#addin nuget:?package=Cake.Boots&version=0.1.0.286-beta
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
string suffix = "-pre1";

Task("Boots")
    .Does(async () =>
    {
        var platform = IsRunningOnWindows() ? "windows" : "macos";
        await Boots ($"https://aka.ms/xamarin-android-commercial-d16-2-{platform}");
    });

Task("Clean")
    .Does(() =>
    {
        foreach (var dir in dirs)
            CleanDirectory(dir);
    });

Task("Build")
    .Does(() =>
    {
        MSBuild(sln, MSBuildSettings());
    });

Task("Install")
    .IsDependentOn("Build")
    .Does(() =>
    {
        MSBuild("./glidex.forms.sample/glidex.forms.sample.csproj", MSBuildSettings().WithTarget("Install").WithTarget("_Run"));
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

Task("AppVeyor")
    .IsDependentOn("Boots")
    .IsDependentOn("NuGet-Package");

RunTarget(target);
