void package(string version, string nuspec, string file, string output)
{
    string configuration = Argument("configuration", "Release");

    var settings   = new NuGetPackSettings
    {
        Verbosity = NuGetVerbosity.Detailed,
        Version = version,
        Files = new[]
        {
            new NuSpecContent { Source = Directory("bin") + Directory(configuration) + File(file), Target = "lib/monoandroid81" },
        },
        OutputDirectory = output
    };
        
    NuGetPack(nuspec, settings);
}

void push(string file)
{
    var apiKey = TransformTextFile ("./.nugetapikey").ToString();

    NuGetPush(file, new NuGetPushSettings 
    {
        Verbosity = NuGetVerbosity.Detailed,
        Source = "nuget.org",
        ApiKey = apiKey
    });
}

MSBuildSettings MSBuildSettings()
{
    var settings = new MSBuildSettings { Configuration = configuration, Verbosity = Verbosity.Diagnostic };

    if (IsRunningOnWindows())
    {
        // Find MSBuild for Visual Studio 2019 and newer
        DirectoryPath vsLatest = VSWhereLatest();
        FilePath msBuildPath = vsLatest?.CombineWithFilePath("./MSBuild/Current/Bin/MSBuild.exe");

        // Find MSBuild for Visual Studio 2017
        if (msBuildPath != null && !FileExists(msBuildPath))
            msBuildPath = vsLatest.CombineWithFilePath("./MSBuild/15.0/Bin/MSBuild.exe");

        // Have we found MSBuild yet?
        if (!FileExists(msBuildPath))
        {
            throw new Exception($"Failed to find MSBuild: {msBuildPath}");
        }

        Information("Building using MSBuild at " + msBuildPath);
        settings.ToolPath = msBuildPath;
    }
    else
    {
        settings.ToolPath = Context.Tools.Resolve("msbuild");
    }

    return settings.WithRestore();
}
