using System;
using System.Runtime.InteropServices;
using System.Security;

namespace KFlearning.Core.Native
{
    [SuppressUnmanagedCodeSecurity]
    internal static class NativeMethods
    {
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool OpenProcessToken(IntPtr processHandle, uint desiredAccess, out TokenSafeHandle tokenHandle);

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool GetTokenInformation(TokenSafeHandle tokenHandle, TOKEN_INFORMATION_CLASS tokenInformationClass,
            IntPtr tokenInformation, uint tokenInformationLength, out uint returnLength);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr hObject);
    }
}