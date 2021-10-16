using System.Collections.Generic;
using KFlearning.TemplateProvider;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace KFlearning.App.Services
{
    public interface ITemplateService
    {
        IEnumerable<ITemplateProvider> GetTemplates();
        void Scaffold(ITemplateProvider template, string outputPath);
    }

    public class TemplateService : ITemplateService
    {
        private readonly ILogger<TemplateService> _logger;
        private readonly List<ITemplateProvider> _templates;

        public TemplateService(ILogger<TemplateService> logger)
        {
            _logger = logger;
            _templates = new List<ITemplateProvider>(Program.Container.GetServices<ITemplateProvider>());
        }

        public IEnumerable<ITemplateProvider> GetTemplates()
        {
            return _templates;
        }

        public void Scaffold(ITemplateProvider template, string outputPath)
        {
            _logger.LogDebug("Scaffolding {0} to {1}", template.Title, outputPath);
            template.Scaffold(outputPath);
        }
    }
}