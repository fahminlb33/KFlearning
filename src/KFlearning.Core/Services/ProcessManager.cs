using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;
using KFlearning.Core.Native;
using Microsoft.Win32;

namespace KFlearning.Core.Services
{
    public interface IProcessManager
    {
        bool IsProcessElevated();
        bool IsUacEnabled();

        void Run(string filename, string args, bool show = false);
    }

    public class ProcessManager : IProcessManager
    {
        public bool IsProcessElevated()
        {
            if (!IsUacEnabled())
            {
                WindowsIdentity identity = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                bool result = principal.IsInRole(WindowsBuiltInRole.Administrator)
                              || principal.IsInRole(0x200); //Domain Administrator
                return result;
            }

            if (!NativeMethods.OpenProcessToken(Process.GetCurrentProcess().Handle, NativeConstants.TOKEN_READ,
                out TokenSafeHandle tokenHandle))
            {
                throw new Win32Exception();
            }

            using (tokenHandle)
            {
                int elevationResultSize = Marshal.SizeOf(Enum.GetUnderlyingType(typeof(TOKEN_ELEVATION_TYPE)));
                IntPtr elevationTypePtr = Marshal.AllocHGlobal(elevationResultSize);

                try
                {
                    bool success = NativeMethods.GetTokenInformation(tokenHandle,
                        TOKEN_INFORMATION_CLASS.TokenElevationType, elevationTypePtr, (uint) elevationResultSize,
                        out uint _);
                    if (success)
                    {
                        var elevationResult = (TOKEN_ELEVATION_TYPE) Marshal.ReadInt32(elevationTypePtr);
                        bool isProcessAdmin = elevationResult == TOKEN_ELEVATION_TYPE.TokenElevationTypeFull;
                        return isProcessAdmin;
                    }
                    else
                    {
                        throw new ApplicationException("Unable to determine the current elevation.");
                    }
                }
                finally
                {
                    if (elevationTypePtr != IntPtr.Zero)
                        Marshal.FreeHGlobal(elevationTypePtr);
                }
            }
        }

        public bool IsUacEnabled()
        {
            using (RegistryKey uacKey = Registry.LocalMachine.OpenSubKey(NativeConstants.UacRegistryKey, false))
            {
                return uacKey != null && uacKey.GetValue(NativeConstants.UacRegistryValue).Equals(1);
            }
        }

        public void Run(string filename, string args, bool show = false)
        {
            Process.Start(filename, args);
        }
    }
}