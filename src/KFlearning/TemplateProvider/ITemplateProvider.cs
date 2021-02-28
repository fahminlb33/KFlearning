namespace KFlearning.TemplateProvider
{
    public interface ITemplateProvider
    {
        string Title { get; }
        void Scaffold(string projectRoot);
    }
}