#addin nuget:?package=Cake.VersionReader

// ------- ARGUMENTS
var target = Argument("target", "all");
var configuration = Argument("configuration", "Release");
var platform = Argument("platform", "x86");

// ----- BUILD SEQUENCE

// Clean build
Task("clean_staging").Does(() => {
	Information("Cleaning staging build...");
	
	CreateDirectory("./build");
	CleanDirectories("./build/app");
	CleanDirectories("./src/**/bin");
	CleanDirectories("./src/**/obj");
});

Task("clean").IsDependentOn("clean_staging").Does(() => {
	Information("Full cleaning...");
	
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
		Runtime = "win-" + platform,
		OutputDirectory = "./build/app"
	});
});

// Build KFlearning and KFmaintenance MSI installer
Task("build_msi").Does(()=>
{
	Information("Platform target: " + platform);
    Information("Builidng KFlearning installer...");
	MSBuild("./src/KFlearning.Setup/KFlearning.Setup.wixproj", new MSBuildSettings 
	{
		Verbosity = Verbosity.Minimal,
		Configuration = configuration,
		PlatformTarget = platform == "x86" ? PlatformTarget.x86 : PlatformTarget.x64
	});

	Information("Determining assembly version...");
	var version = GetVersionNumber("./build/app/KFlearning.App.exe");

	Information("Moving build artifact...");
	MoveFiles("./src/KFlearning.Setup/bin/**/*.msi", "./build");
	MoveFile("./build/KFlearning.Setup.msi", $"./build/KFlearning-{version}_{platform}.msi");
});

// Build all
Task("all")
	.IsDependentOn("clean_staging")
	.IsDependentOn("build_apps")
	.IsDependentOn("build_msi");

// Run target from command line
RunTarget(target);
