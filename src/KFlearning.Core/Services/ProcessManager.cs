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
                var identity = WindowsIdentity.GetCurrent();
                var principal = new WindowsPrincipal(identity);
                var result = principal.IsInRole(WindowsBuiltInRole.Administrator)
                             || principal.IsInRole(0x200); //Domain Administrator
                return result;
            }

            if (!NativeMethods.OpenProcessToken(Process.GetCurrentProcess().Handle, NativeConstants.TOKEN_READ,
                out var tokenHandle))
            {
                throw new Win32Exception();
            }

            using (tokenHandle)
            {
                var elevationResultSize = Marshal.SizeOf(Enum.GetUnderlyingType(typeof(TOKEN_ELEVATION_TYPE)));
                var elevationTypePtr = Marshal.AllocHGlobal(elevationResultSize);

                try
                {
                    var success = NativeMethods.GetTokenInformation(tokenHandle,
                        TOKEN_INFORMATION_CLASS.TokenElevationType, elevationTypePtr, (uint) elevationResultSize,
                        out _);
                    if (success)
                    {
                        var elevationResult = (TOKEN_ELEVATION_TYPE) Marshal.ReadInt32(elevationTypePtr);
                        var isProcessAdmin = elevationResult == TOKEN_ELEVATION_TYPE.TokenElevationTypeFull;
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
                    {
                        Marshal.FreeHGlobal(elevationTypePtr);
                    }
                }
            }
        }

        public bool IsUacEnabled()
        {
            using (var uacKey = Registry.LocalMachine.OpenSubKey(NativeConstants.UacRegistryKey, false))
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