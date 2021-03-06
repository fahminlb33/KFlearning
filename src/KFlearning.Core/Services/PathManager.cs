using KFlearning.Core.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace KFlearning.Core.Services
{
    public interface IPathManager
    {
        bool IsVscodeInstalled { get; }
        bool IsKfMingwInstalled { get; }
        bool IsFlutterInstalled { get; }

        string GetPath(PathKind kind, bool forwardSlash = false);
    }

    public class PathManager : IPathManager
    {
        private Dictionary<PathName, string> _cachedPaths = null;

        private enum PathName
        {
            SystemRoot,
            DocumentRoot,
            AppRoot,
            Vscode,
            KFmingw,
            Flutter
        }

        #region Properies
        
        public bool IsVscodeInstalled => _cachedPaths.ContainsKey(PathName.Vscode);
        public bool IsKfMingwInstalled => _cachedPaths.ContainsKey(PathName.KFmingw);
        public bool IsFlutterInstalled => _cachedPaths.ContainsKey(PathName.Flutter);

        #endregion

        public PathManager()
        {
            _cachedPaths = new Dictionary<PathName, string>
            {
                {PathName.SystemRoot, Path.GetPathRoot(Environment.SystemDirectory)},
                {PathName.DocumentRoot, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)},
                {PathName.AppRoot, Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}
            };

            FindKfMingw();
            FindVscode();
            FindFlutter();
        }

        public string GetPath(PathKind kind, bool forwardSlash = false)
        {
            string path;
            switch (kind)
            {
                case PathKind.DefaultProjectRoot:
                    path = Path.Combine(_cachedPaths[PathName.DocumentRoot], "KFlearning");
                    break;
                case PathKind.PersistanceDirectory:
                    path = Path.Combine(_cachedPaths[PathName.DocumentRoot], @"KFlearning\settings");
                    break;
                case PathKind.WallpaperPath:
                    path = Path.Combine(_cachedPaths[PathName.SystemRoot], "wallpaper.jpg");
                    break;
                case PathKind.VisualStudioCodeExecutable:
                    path = _cachedPaths[PathName.Vscode];
                    break;
                case PathKind.MingwInclude1Directory:
                    path = Path.Combine(_cachedPaths[PathName.KFmingw], @"include");
                    break;
                case PathKind.MingwInclude2Directory:
                    path = Path.Combine(_cachedPaths[PathName.KFmingw], @"i686-w64-mingw32\include");
                    break;
                case PathKind.MingwGXXExecutable:
                    path = Path.Combine(_cachedPaths[PathName.KFmingw], @"bin\g++.exe");
                    break;
                case PathKind.MingwGDBExecutable:
                    path = Path.Combine(_cachedPaths[PathName.KFmingw], @"bin\gdb.exe");
                    break;
                case PathKind.KFserverExecutable:
                    path = Path.Combine(_cachedPaths[PathName.AppRoot], @"kfserver.exe");
                    break;
                case PathKind.FlutterInstallDirectory:
                    path = Path.Combine(_cachedPaths[PathName.SystemRoot], @"src\flutter");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(kind), kind, null);
            }

            return forwardSlash ? path.Replace('\\', '/') : path;
        }

        #region Private Methods

        private void FindKfMingw()
        {
            // find installation on default path
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "KF-MinGW");
            if (Directory.Exists(path))
            {
                _cachedPaths.Add(PathName.KFmingw, path);
                return;
            }

            // find in env path
            path = PathHelpers.GetFullPathToEnv("g++.exe");
            if (path != null)
            {
                _cachedPaths.Add(PathName.KFmingw, path);
            }
        }

        private void FindVscode()
        {
            // find in user dir
            var userDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var path = Path.Combine(userDir, @"Programs\Microsoft VS Code\code.exe");
            if (File.Exists(path))
            {
                _cachedPaths.Add(PathName.Vscode, path);
                return;
            }

            // find in env path
            path = PathHelpers.GetFullPathToEnv("code.cmd");
            if (path != null)
            {
                 path = Path.Combine(Path.GetDirectoryName(path).Remove(path.Length - 12, 3), "code.exe");
                _cachedPaths.Add(PathName.Vscode, path);
            }
        }

        private void FindFlutter()
        {
            // find installation on default path
            var path = Path.Combine(_cachedPaths[PathName.SystemRoot], @"src\bin", "flutter.exe");
            if (File.Exists(path))
            {
                _cachedPaths.Add(PathName.Flutter, path);
                return;
            }

            // find in env path
            path = PathHelpers.GetFullPathToEnv("flutter.exe");
            if (path != null)
            {
                _cachedPaths.Add(PathName.Flutter, path);
            }
        }

        #endregion
    }
}