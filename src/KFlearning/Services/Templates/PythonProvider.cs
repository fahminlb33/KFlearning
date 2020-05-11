// SOLUTION : KFlearning
// PROJECT  : KFlearning
// FILENAME : PythonProvider.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

namespace KFlearning.Services.Templates
{
    public class PythonProvider : TemplateProviderBase, ITemplateProvider
    {
        public string Title => "Python/Jupyter Notebook";

        public void Provide(string projectRoot)
        {
            ProjectRoot = projectRoot;

            // program.py
            WriteFile("program.py", TR.PY_program);
        }
    }
}