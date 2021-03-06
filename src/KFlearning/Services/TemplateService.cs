using System.Collections.Generic;
using Castle.Core.Logging;
using KFlearning.TemplateProvider;

namespace KFlearning.Services
{
    public interface ITemplateService
    {
        IEnumerable<ITemplateProvider> GetTemplates();
        void Scaffold(ITemplateProvider template, string outputPath);
    }

    public class TemplateService : ITemplateService
    {
        private readonly ILogger _logger;
        private readonly List<ITemplateProvider> _templates;

        public TemplateService(ILogger logger)
        {
            _logger = logger;
            _templates = new List<ITemplateProvider>(Program.Container.ResolveAll<ITemplateProvider>());
        }

        public IEnumerable<ITemplateProvider> GetTemplates()
        {
            return _templates;
        }

        public void Scaffold(ITemplateProvider template, string outputPath)
        {
            _logger.DebugFormat("Scaffolding {0} to {1}", template.Title, outputPath);
            template.Scaffold(outputPath);
        }
    }
}