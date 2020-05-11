// SOLUTION : KFlearning
// PROJECT  : KFlearning.Core
// FILENAME : TokenSafeHandle.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

using Microsoft.Win32.SafeHandles;

namespace KFlearning.Core.Native
{
    public class TokenSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        public TokenSafeHandle() : base(true)
        {
        }

        protected override bool ReleaseHandle()
        {
            return NativeMethods.CloseHandle(handle);
        }
    }
}