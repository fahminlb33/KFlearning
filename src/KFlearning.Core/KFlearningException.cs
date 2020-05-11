// SOLUTION : KFlearning
// PROJECT  : KFlearning.Core
// FILENAME : KFlearningException.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

using System;

namespace KFlearning.Core
{
    [Serializable]
    public class KFlearningException : Exception
    {
        public KFlearningException(string message) : base(message)
        {
        }

        public KFlearningException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}