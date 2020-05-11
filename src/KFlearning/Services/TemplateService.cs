// SOLUTION : KFlearning
// PROJECT  : KFlearning
// FILENAME : TemplateService.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

using System.Collections.Generic;
using Castle.Windsor;
using KFlearning.Core.Services;

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
            template.Provide(outputPath);
        }
    }
}