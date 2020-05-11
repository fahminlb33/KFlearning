// SOLUTION : KFlearning
// PROJECT  : KFlearning.Core
// FILENAME : PathManager.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

#region

using System;
using System.IO;
using System.Linq;

#endregion

namespace KFlearning.Core.Services
{
    public interface IPathManager
    {
        string GetPath(PathKind kind, bool forwardSlash = false);
        string StripInvalidPathName(string path);
    }

    public class PathManager : IPathManager
    {
        private static readonly string SystemRoot = Path.GetPathRoot(Environment.SystemDirectory);
        private static readonly string DocumentRoot = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private static readonly char[] InvalidFileNameChars = Path.GetInvalidFileNameChars();
        private string _cachedVscodePath;
        private string _cachedKfMingwPath;

        public string GetPath(PathKind kind, bool forwardSlash = false)
        {
            string path;
            switch (kind)
            {
                case PathKind.DefaultProjectRoot:
                    path = Path.Combine(DocumentRoot, "KFlearning");
                    break;
                case PathKind.PersistanceDirectory:
                    path = Path.Combine(DocumentRoot, @"KFlearning\settings");
                    break;
                case PathKind.WallpaperPath:
                    path = Path.Combine(SystemRoot, "wallpaper.jpg");
                    break;
                case PathKind.VisualStudioCodeExecutable:
                    path = FindVscode();
                    break;
                case PathKind.MingwInclude1Directory:
                    path = Path.Combine(FindKfMingw(), @"include");
                    break;
                case PathKind.MingwInclude2Directory:
                    path = Path.Combine(FindKfMingw(), @"i686-w64-mingw32\include");
                    break;
                case PathKind.MingwGXXExecutable:
                    path = Path.Combine(FindKfMingw(), @"bin\g++.exe");
                    break;
                case PathKind.MingwGDBExecutable:
                    path = Path.Combine(FindKfMingw(), @"bin\gdb.exe");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(kind), kind, null);
            }

            return forwardSlash ? path.Replace('\\', '/') : path;
        }

        public string StripInvalidPathName(string path)
        {
            return InvalidFileNameChars.Aggregate(path, (current, x) => current.Replace(x, '_'));
        }

        private string FindKfMingw()
        {
            if (_cachedKfMingwPath != null) return _cachedKfMingwPath;

            // find installation
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "KF-MinGW");
            if (!Directory.Exists(path)) return null;

            _cachedKfMingwPath = path;
            return path;
        }

        private string FindVscode()
        {
            if (_cachedVscodePath != null) return _cachedVscodePath;

            // find in env path
            var userEnv = Environment.GetEnvironmentVariable("path");
            var path = userEnv?.Split(Path.PathSeparator).FirstOrDefault(x => x.Contains("Microsoft VS Code"));
            if (path != null)
            {
                path = Path.Combine(path.Substring(0, path.Length - 4), "code.exe");
                _cachedVscodePath = path;

                return path;
            }

            // find in user dir
            var userDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var userInstall = Path.Combine(userDir, @"Programs\Microsoft VS Code\code.exe");
            if (File.Exists(userInstall))
            {
                _cachedVscodePath = userInstall;
                return userInstall;
            }

            // not found
            return null;
        }
    }
}