using System.Collections.Generic;
using System.Linq;
using KFlearning.Core.Services;

namespace KFlearning.App.Services
{
    public interface IHistoryService : IUsesPersistence
    {
        bool RecordHistory { get; set; }

        void Add(Project project);
        void Clear();
        IEnumerable<Project> GetAll();
    }

    public class HistoryService : IHistoryService
    {
        private const int HistorySize = 20;
        private const string HistorySettingsName = "History.Settings";

        private readonly List<Project> _projects = new List<Project>();
        private readonly IPersistanceStorage _storage;

        public bool RecordHistory { get; set; }

        public HistoryService(IPersistanceStorage storage)
        {
            _storage = storage;
            Load();
        }

        public void Add(Project project)
        {
            if (!RecordHistory)
            {
                return;
            }

            _projects.RemoveAll(x => x.Path == project.Path);
            _projects.Add(project);

            EnsureSize();
        }

        public void Clear()
        {
            if (!RecordHistory)
            {
                return;
            }

            _projects.Clear();
        }

        public IEnumerable<Project> GetAll()
        {
            return !RecordHistory ? Enumerable.Empty<Project>() : _projects;
        }

        private void EnsureSize()
        {
            if (!RecordHistory)
            {
                return;
            }

            if (_projects.Count <= HistorySize)
            {
                return;
            }

            _projects.RemoveRange(HistorySize - 1, _projects.Count - HistorySize);
        }

        private HistorySettings CreateDefaultSettings()
        {
            return new HistorySettings
            {
                Recording = true,
                Projects = new List<Project>()
            };
        }

        #region IUsesPersistence Implementation

        public void Load()
        {
            var settings = _storage.Retrieve<HistorySettings>(HistorySettingsName) ?? CreateDefaultSettings();
            if (!settings.Recording)
            {
                return;
            }

            RecordHistory = true;

            _projects.Clear();
            _projects.AddRange(settings.Projects);
        }

        public void Save()
        {
            var settings = new HistorySettings
            {
                Recording = RecordHistory,
                Projects = _projects.ToList()
            };

            _storage.Store(HistorySettingsName, settings);
        }

        #endregion
    }
}