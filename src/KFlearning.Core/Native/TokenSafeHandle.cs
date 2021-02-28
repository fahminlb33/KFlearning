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