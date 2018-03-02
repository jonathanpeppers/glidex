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
string sln = "./glide.sln";
string version = "0.1.0";
string suffix = "-beta";

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
    .IsDependentOn("NUnit")
    .Does(() =>
    {
        var settings   = new NuGetPackSettings
        {
            Verbosity = NuGetVerbosity.Detailed,
            Version = version + suffix,
            Files = new [] 
            {
                new NuSpecContent { Source = dirs[1] + File("glidex.dll"), Target = "lib/monoandroid80" },
            },
            OutputDirectory = dirs[0]
        };
            
        NuGetPack("./glidex/glidex.nuspec", settings);
    });

Task("NuGet-Push-GlideX")
    .IsDependentOn("NuGet-Package-GlideX")
    .Does(() =>
    {
        var apiKey = TransformTextFile ("./.nugetapikey").ToString();

        NuGetPush("./build/glidex." + version + suffix + ".nupkg", new NuGetPushSettings 
        {
            Verbosity = NuGetVerbosity.Detailed,
            Source = "nuget.org",
            ApiKey = apiKey
        });
    });

Task("NuGet-Package")
    .IsDependentOn("NuGet-Package-GlideX");

Task("Default")
    .IsDependentOn("NuGet-Package");

RunTarget(target);