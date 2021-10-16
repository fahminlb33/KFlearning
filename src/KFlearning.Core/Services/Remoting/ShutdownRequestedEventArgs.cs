using System;
using System.Net;

namespace KFlearning.Core.Remoting
{
    public class ShutdownRequestedEventArgs : EventArgs
    {
        public IPAddress Address { get; set; }
        public string Cluster { get; set; }
    }
}