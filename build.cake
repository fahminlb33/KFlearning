// ------- ARGUMENTS
var target = Argument("target", "Build-Incremental");
var configuration = Argument("configuration", "Release");

// ------ BUILD CONFIGURATION
var buildSettings = new MSBuildSettings
{
	Verbosity = Verbosity.Minimal,
	ToolVersion = MSBuildToolVersion.VS2019,
	Configuration = configuration,
	PlatformTarget = PlatformTarget.x86
};

// ----- BUILD SEQUENCE

// Clean build
Task("Clean")
.Does(() => {
	Information("Cleaning /build folder...");
	
	CleanDirectories("./build");

	CreateDirectory("./build");
	CreateDirectory("./build/kflearning");
	CreateDirectory("./build/kfmaintenance");
});

// Build KFlearning
Task("Build-Apps")
.Does(() =>
{
	Information("Restoring Nuget dependecies...");
	NuGetRestore("./KFlearning.sln");

	Information("Builidng solution...");
	MSBuild("./KFlearning.sln", buildSettings);

	Information("Copying build output...");
	CopyFile($"./src/KFserver/bin/Win32/{configuration}/KFserver.exe", "./build/kflearning/KFserver.exe");
	CopyDirectory($"./src/KFlearning/bin/x86/{configuration}", "./build/kflearning");
	CopyDirectory($"./src/KFmaintenance/bin/x86/{configuration}", "./build/kfmaintenance");
});

// Build KF-MinGW MSI installer
Task("Build-MSI-KF_MinGW")
.Does(()=>
{
    Information("Builidng setup installer...");
	MSBuild("./src/KFlearning.Mingw.Setup/KFlearning.Mingw.Setup.wixproj", buildSettings);

	Information("Moving setup installer...");
	MoveFile($"./src/KFlearning.Mingw.Setup/bin/{configuration}/KFlearning.Mingw.Setup.msi", "./build/KFlearning.Mingw.Setup.msi");
});

// Build KFlearning and KFmaintenance MSI installer
Task("Build-MSI-Apps")
.Does(()=>
{
    Information("Builidng KFlearning installer...");
	MSBuild("./src/KFlearning.Setup/KFlearning.Setup.wixproj", buildSettings);
	MoveFile($"./src/KFlearning.Setup/bin/{configuration}/KFlearning.Setup.msi", "./build/KFlearning.Setup.msi");

	Information("Builidng KFmaintenance installer...");
	MSBuild("./src/KFmaintenance.Setup/KFmaintenance.Setup.wixproj", buildSettings);
	MoveFile($"./src/KFmaintenance.Setup/bin/{configuration}/KFmaintenance.Setup.msi", "./build/KFmaintenance.Setup.msi");
});

// Build all
Task("Build-All")
.IsDependentOn("Clean")
.IsDependentOn("Build-Apps")
.IsDependentOn("Build-MSI-Apps")
.IsDependentOn("Build-MSI-KF_MinGW");

// Build incremental
Task("Build-Incremental")
.IsDependentOn("Clean")
.IsDependentOn("Build-Apps")
.IsDependentOn("Build-MSI-Apps");

// Run target from command line
RunTarget(target);