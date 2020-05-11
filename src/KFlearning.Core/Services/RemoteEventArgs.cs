// SOLUTION : KFlearning
// PROJECT  : KFlearning.Core
// FILENAME : RemoteEventArgs.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

using System;
using System.Net;

namespace KFlearning.Core.Services
{
    public class RemoteEventArgs : EventArgs
    {
        public IPAddress Address { get; set; }
    }
}