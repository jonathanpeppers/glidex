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
