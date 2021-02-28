using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace KFlearning.Core.Services
{
    public interface IPathManager
    {
        string GetPath(PathKind kind, bool forwardSlash = false);
        string StripInvalidPathName(string path);
        bool IsVscodeInstalled();
        bool IsKfMingwInstalled();
    }

    public class PathManager : IPathManager
    {
        private static readonly string SystemRoot = Path.GetPathRoot(Environment.SystemDirectory);
        private static readonly string DocumentRoot = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private static readonly string AppRoot = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
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
                case PathKind.KFserverExecutable:
                    path = Path.Combine(AppRoot, @"kfserver.exe");
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

        public bool IsVscodeInstalled()
        {
            return FindVscode() != null;
        }

        public bool IsKfMingwInstalled()
        {
            return FindKfMingw() != null;
        }

        private string FindKfMingw()
        {
            if (_cachedKfMingwPath != null) return _cachedKfMingwPath;

            // find installation on default path
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "KF-MinGW");
            if (!Directory.Exists(path)) return null;

            // find in env path
            if (GetFullPathToEnv("g++.exe") == null) return null;

            _cachedKfMingwPath = path;
            return _cachedKfMingwPath;
        }

        private string FindVscode()
        {
            if (_cachedVscodePath != null) return _cachedVscodePath;

            // find in user dir
            var userDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var userInstall = Path.Combine(userDir, @"Programs\Microsoft VS Code\code.exe");
            if (File.Exists(userInstall))
            {
                _cachedVscodePath = userInstall;
                return userInstall;
            }

            // find in env path
            var userEnv = GetFullPathToEnv("code.cmd");
            if (userEnv != null)
            {
                _cachedVscodePath = Path.Combine(Path.GetDirectoryName(userEnv).Remove(userEnv.Length - 12, 3), "code.exe");
                return _cachedVscodePath;
            }

            // not found
            return null;
        }

        private static string GetFullPathToEnv(string fileName)
        {
            if (File.Exists(fileName))
            {
                return Path.GetFullPath(fileName);
            }

            var values = Environment.GetEnvironmentVariable("PATH");
            return values?.Split(Path.PathSeparator).Select(path => Path.Combine(path, fileName)).FirstOrDefault(File.Exists);
        }
    }
}