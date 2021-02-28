namespace KFlearning.Core.Services
{
    public interface IUsesPersistance
    {
        void Load();
        void Save();
    }
}