// ------- ARGUMENTS
var target = Argument("target", "all");
var configuration = Argument("configuration", "Release");

// ----- BUILD SEQUENCE

// Clean build
Task("clean").Does(() => {
	Information("Cleaning /build folder...");
	
	CreateDirectory("./build");
	CleanDirectories("./build");
});

// Build KFlearning
Task("build_apps").Does(() =>
{
	Information("Publishing KFlearning.App...");
	DotNetCorePublish("./src/KFlearning.App/KFlearning.App.csproj", new DotNetCorePublishSettings
	{
		Verbosity = DotNetCoreVerbosity.Minimal,
		Configuration = configuration,
		NoBuild = false,   // implicit build
		NoRestore = false, // implicit nuget restore
		PublishReadyToRun = true,
		SelfContained = false,
		Runtime = "win-x86",
		OutputDirectory = "./build/app"
	});
});

// Build KFlearning and KFmaintenance MSI installer
Task("build_msi").Does(()=>
{
    Information("Builidng KFlearning installer...");
	MSBuild("./src/KFlearning.Setup/KFlearning.Setup.wixproj", new MSBuildSettings 
	{
		Verbosity = Verbosity.Minimal,
		Configuration = configuration,
		PlatformTarget = PlatformTarget.x86
	});

	Information("Moving build artifact...");
	MoveFile($"./src/KFlearning.Setup/bin/{configuration}/KFlearning.Setup.msi", "./build/KFlearning.Setup.msi");
});

// Build all
Task("all")
	.IsDependentOn("clean")
	.IsDependentOn("build_apps")
	.IsDependentOn("build_msi");

// Run target from command line
RunTarget(target);
