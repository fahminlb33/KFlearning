// SOLUTION : KFlearning
// PROJECT  : KFlearning.Core
// FILENAME : IUsesPersistance.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

namespace KFlearning.Core.Services
{
    public interface IUsesPersistance
    {
        void Load();
        void Save();
    }
}