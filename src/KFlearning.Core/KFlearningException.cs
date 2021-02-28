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