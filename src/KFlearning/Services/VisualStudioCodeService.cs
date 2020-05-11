// SOLUTION : KFlearning
// PROJECT  : KFlearning
// FILENAME : VisualStudioCodeService.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

#region

using KFlearning.Core.Services;

#endregion

namespace KFlearning.Services
{
    public interface IVisualStudioCodeService
    {
        void OpenFolder(string path);
    }

    public class VisualStudioCodeService : IVisualStudioCodeService
    {
        private readonly IProcessManager _processManager;
        private readonly IPathManager _path;

        public VisualStudioCodeService(IProcessManager processManager, IPathManager path)
        {
            _processManager = processManager;
            _path = path;
        }

        public void OpenFolder(string path)
        {
            var vscode = _path.GetPath(PathKind.VisualStudioCodeExecutable);
            _processManager.Run(vscode, $"\"{path}\"");
        }
    }
}