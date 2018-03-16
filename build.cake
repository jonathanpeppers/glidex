//Tools
#tool nuget:?package=GitReleaseNotes

//Other files
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
};
string output = dirs[0];
string sln = "./glidex.sln";
string version = "0.1.1";
string suffix = "-beta";
string releaseNotes = "./ReleaseNotes.md";
string releaseNotesText = "";

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

Task("NuGet-Package-GlideX")
    .IsDependentOn("Build")
    .Does(() =>
    {
        package(version + suffix, "./glidex/glidex.nuspec", "glidex.dll", output);
    });

Task("NuGet-Push-GlideX")
    .IsDependentOn("NuGet-Package-GlideX")
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
    .IsDependentOn("NuGet-Package-GlideX-Forms")
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

Task("Release-Notes")
    .Does(() =>
    {
        var releasePath = MakeAbsolute(File(releaseNotes));
        GitReleaseNotes(releasePath, new GitReleaseNotesSettings
        {
            WorkingDirectory = ".",
            Version = version,
            AllLabels = true
        });

        releaseNotesText = System.IO.File.ReadAllText(releasePath.FullPath);
        Information(releaseNotesText);
    });

Task("Default")
    .IsDependentOn("NuGet-Package");

RunTarget(target);