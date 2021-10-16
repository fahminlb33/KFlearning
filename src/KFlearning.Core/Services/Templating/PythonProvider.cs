using KFlearning.Core.Services.Templating;

namespace KFlearning.TemplateProvider
{
    public class PythonProvider : TemplateProviderBase, ITemplateProvider
    {
        public string Title => "Python/Jupyter Notebook";

        public void Scaffold(string projectRoot)
        {
            ProjectRoot = projectRoot;

            // program.py
            WriteFile("program.py", TemplateResource.PY_program);
        }
    }
}