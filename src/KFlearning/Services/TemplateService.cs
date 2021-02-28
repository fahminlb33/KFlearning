using System.Collections.Generic;
using KFlearning.TemplateProvider;

namespace KFlearning.Services
{
    public interface ITemplateService
    {
        IEnumerable<ITemplateProvider> GetTemplates();
        void Extract(ITemplateProvider template, string outputPath);
    }

    public class TemplateService : ITemplateService
    {
        private readonly List<ITemplateProvider> _templates;

        public TemplateService()
        {
            _templates = new List<ITemplateProvider>(Program.Container.ResolveAll<ITemplateProvider>());
        }

        public IEnumerable<ITemplateProvider> GetTemplates()
        {
            return _templates;
        }

        public void Extract(ITemplateProvider template, string outputPath)
        {
            template.Scaffold(outputPath);
        }
    }
}