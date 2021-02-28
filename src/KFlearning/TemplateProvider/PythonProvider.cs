namespace KFlearning.TemplateProvider
{
    public class PythonProvider : TemplateProviderBase, ITemplateProvider
    {
        public string Title => "Python/Jupyter Notebook";

        public void Scaffold(string projectRoot)
        {
            ProjectRoot = projectRoot;

            // program.py
            WriteFile("program.py", TR.PY_program);
        }
    }
}