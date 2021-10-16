namespace KFlearning.Core.Services
{
    public interface IUsesPersistence
    {
        void Load();
        void Save();
    }
}