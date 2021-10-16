using System.IO;
using System.Text.Json;
using KFlearning.Core.Extensions;

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

        public PersistanceStorage(IPathManager path)
        {
            _path = path;

            var dirPath = _path.GetPath(PathKind.PersistanceDirectory);
            Directory.CreateDirectory(dirPath);
        }

        public void Store(string name, object value)
        {
            var path = Path.Combine(_path.GetPath(PathKind.PersistanceDirectory), name + PathHelpers.JsonExtension);
            using var writer = File.OpenWrite(path);

            JsonSerializer.Serialize(writer, value, new JsonSerializerOptions { WriteIndented = true });
        }

        public T Retrieve<T>(string name)
        {
            try
            {
                var path = Path.Combine(_path.GetPath(PathKind.PersistanceDirectory), name + PathHelpers.JsonExtension);
                if (!File.Exists(path))
                {
                    return default;
                }

                using var reader = File.OpenRead(path);
                return JsonSerializer.Deserialize<T>(reader);
            }
            catch
            {
                return default;
            }
        }
    }
}