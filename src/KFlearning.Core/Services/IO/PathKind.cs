namespace KFlearning.Core.Services
{
    public enum PathKind
    {
        // application path
        ProjectRoot,
        SettingsRoot,
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
        FlutterInstallRoot,
    }
}