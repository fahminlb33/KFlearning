using KFlearning.Core.Services.Templating;

namespace KFlearning.TemplateProvider
{
    public class WebProvider : TemplateProviderBase, ITemplateProvider
    {
        public string Title => "Web (PHP/HTML/CSS/JS)";

        public void Scaffold(string projectRoot)
        {
            ProjectRoot = projectRoot;

            // program.cpp
            WriteFile("index.php", TemplateResource.WEB_index);
        }
    }
}