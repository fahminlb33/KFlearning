// ------- ARGUMENTS
var target = Argument("target", "Build-All");
var configuration = Argument("configuration", "Release");

// ------ BUILD CONFIGURATION
var buildSettingsNetCore = new DotNetCoreBuildSettings
{
	Verbosity = DotNetCoreVerbosity.Minimal,
	Configuration = configuration
};

var buildSettingsMSBuild = new MSBuildSettings 
{
	Verbosity = Verbosity.Minimal,
	Configuration = configuration
};

// ----- BUILD SEQUENCE

// Clean build
Task("Clean")
.Does(() => {
	Information("Cleaning /build folder...");
	
	CreateDirectory("./build");
	CleanDirectories("./build");
});

// Build KFlearning
Task("Build-Apps")
.Does(() =>
{
	Information("Restoring Nuget dependecies...");
	DotNetCoreRestore("./KFlearning.sln");

	Information("Builidng solution...");
	DotNetCoreBuild("./KFlearning.sln", buildSettingsNetCore);

	Information("Copying build output...");
	CopyDirectory($"./src/KFlearning.App/bin/{configuration}/net6.0-windows", "./build");
});

// Build KFlearning and KFmaintenance MSI installer
Task("Build-MSI-Apps")
.Does(()=>
{
    Information("Builidng KFlearning installer...");
	MSBuild("./src/KFlearning.Setup/KFlearning.Setup.wixproj", buildSettingsMSBuild);
	MoveFile($"./src/KFlearning.Setup/bin/{configuration}/KFlearning.Setup.msi", "./build/KFlearning.Setup.msi");
});

// Build all
Task("Build-All")
.IsDependentOn("Clean")
.IsDependentOn("Build-Apps")
.IsDependentOn("Build-MSI-Apps");

// Run target from command line
RunTarget(target);