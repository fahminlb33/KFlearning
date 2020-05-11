// SOLUTION : KFlearning
// PROJECT  : KFlearning
// FILENAME : PhpProvider.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

namespace KFlearning.Services.Templates
{
    public class PhpProvider : TemplateProviderBase, ITemplateProvider
    {
        public string Title => "Web (PHP/HTML/CSS/JS)";

        public void Provide(string projectRoot)
        {
            ProjectRoot = projectRoot;

            // program.cpp
            WriteFile("index.php", TR.WEB_index);
        }
    }
}