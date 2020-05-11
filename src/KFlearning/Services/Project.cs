// SOLUTION : KFlearning
// PROJECT  : KFlearning
// FILENAME : Project.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

using KFlearning.Core;

namespace KFlearning.Services
{
    public class Project
    {
        public string Name { get; set; }
        public ITemplateProvider Template { get; set; }
        public string Path { get; set; }

        public override string ToString()
        {
            return $"{Name} ({Helpers.TrimLongText(Path)})";
        }
    }
}