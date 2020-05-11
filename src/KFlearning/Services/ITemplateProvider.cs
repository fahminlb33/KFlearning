// SOLUTION : KFlearning
// PROJECT  : KFlearning
// FILENAME : ITemplateProvider.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

namespace KFlearning.Services
{
    public interface ITemplateProvider
    {
        string Title { get; }
        void Provide(string projectRoot);
    }
}