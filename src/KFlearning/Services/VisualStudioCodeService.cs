using KFlearning.Core.Services;

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