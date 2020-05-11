// SOLUTION : KFlearning
// PROJECT  : KFlearning.Core
// FILENAME : PersistanceStorage.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

using System.IO;
using Newtonsoft.Json;

namespace KFlearning.Core.Services
{
    public interface IPersistanceStorage
    {
        void Store(string name, object value);
        T Retrieve<T>(string name);
    }

    public class PersistanceStorage : IPersistanceStorage
    {
        private readonly IPathManager _path;

        private readonly JsonSerializer _serializer = new JsonSerializer
        {
            Formatting = Formatting.Indented
        };

        public PersistanceStorage(IPathManager path)
        {
            _path = path;
            var dirPath = _path.GetPath(PathKind.PersistanceDirectory);
            Directory.CreateDirectory(dirPath);
        }

        public void Store(string name, object value)
        {
            var path = Path.Combine(_path.GetPath(PathKind.PersistanceDirectory), name + ".kfl");
            using (var writer = new StreamWriter(path))
            {
                _serializer.Serialize(writer, value);
            }
        }

        public T Retrieve<T>(string name)
        {
            try
            {
                var path = Path.Combine(_path.GetPath(PathKind.PersistanceDirectory), name + ".kfl");
                if (!File.Exists(path)) return default;

                using (var reader = new StreamReader(path))
                using (var jsonReader = new JsonTextReader(reader))
                {
                    return _serializer.Deserialize<T>(jsonReader);
                }
            }
            catch
            {
                return default;
            }
        }
    }
}