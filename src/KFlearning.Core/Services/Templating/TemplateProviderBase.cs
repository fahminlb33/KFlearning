using System;
using System.IO;
using System.Text;

namespace KFlearning.TemplateProvider
{
    public abstract class TemplateProviderBase
    {
        protected string ProjectRoot { get; set; }

        protected void EnsureOutputExist(string relativePath)
        {
            var path = Path.Combine(ProjectRoot, relativePath);
            Directory.CreateDirectory(Path.GetDirectoryName(path) ?? throw new InvalidOperationException());
        }

        protected void WriteFile(string relativePath, string content)
        {
            EnsureOutputExist(relativePath);

            var path = Path.Combine(ProjectRoot, relativePath);
            File.WriteAllText(path, content);
        }

        protected string Transform(string template, Func<StringBuilder, StringBuilder> transformFunc)
        {
            return transformFunc.Invoke(new StringBuilder(template)).ToString();
        }
    }
}