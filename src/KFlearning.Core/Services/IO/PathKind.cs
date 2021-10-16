namespace KFlearning.Core.Services
{
    public enum PathKind
    {
        // application path
        DefaultProjectRoot,
        PersistanceDirectory,
        KFserverExecutable,

        // system paths
        WallpaperPath,

        // vscode
        VisualStudioCodeExecutable,
        
        // mingw
        MingwInclude1Directory,
        MingwInclude2Directory,
        MingwGXXExecutable,
        MingwGDBExecutable,

        // flutter
        FlutterInstallDirectory,
    }
}