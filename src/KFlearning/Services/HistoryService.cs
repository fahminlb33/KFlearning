// SOLUTION : KFlearning
// PROJECT  : KFlearning
// FILENAME : HistoryService.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

using System.Collections.Generic;
using System.Linq;
using KFlearning.Core.Services;

namespace KFlearning.Services
{
    public interface IHistoryService : IUsesPersistance
    {
        bool RecordHistory { get; set; }

        void Add(Project project);
        void Clear();
        IEnumerable<Project> GetAll();
    }

    public class HistoryService : IHistoryService
    {
        private const int HistorySize = 10;
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
            if (!RecordHistory) return;
            _projects.RemoveAll(x => x.Path == project.Path);
            _projects.Add(project);
            EnsureSize();
        }

        public void Clear()
        {
            if (!RecordHistory) return;
            _projects.Clear();
        }

        public IEnumerable<Project> GetAll()
        {
            return !RecordHistory ? Enumerable.Empty<Project>() : _projects;
        }

        private void EnsureSize()
        {
            if (!RecordHistory) return;
            if (_projects.Count <= HistorySize) return;

            _projects.RemoveRange(HistorySize - 1, _projects.Count - HistorySize);
        }

        private HistorySettings CreateDefaultSettings()
        {
            return new HistorySettings {Recording = true, Projects = new List<Project>()};
        }

        #region IUsesPersistance Implementation

        public void Load()
        {
            var settings = _storage.Retrieve<HistorySettings>(HistorySettingsName) ?? CreateDefaultSettings();
            if (!settings.Recording) return;

            _projects.Clear();
            _projects.AddRange(settings.Projects);
            RecordHistory = true;
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