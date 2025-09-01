using System.ComponentModel;
using System.Runtime.InteropServices;
using System;
using System.Threading;

namespace Caffeine.Core
{
    internal static partial class CaffeineController
    {
        private static readonly Lock _sync = new();

        public static void KeepSystemAwake(bool keepDisplayAwake = false)
        {
            lock (_sync)
            {
                EXECUTION_STATE state = keepDisplayAwake ?
                    EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_SYSTEM_REQUIRED | EXECUTION_STATE.ES_DISPLAY_REQUIRED :
                    EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_SYSTEM_REQUIRED;
                var ret = (uint)SetThreadExecutionState(state);
                if (ret == 0)
                    throw new Win32Exception(Marshal.GetLastWin32Error(), "SetThreadExecutionState FAIL");
            }
        }

        [LibraryImport("kernel32.dll", SetLastError = true)]
        private static partial EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        [Flags]
        private enum EXECUTION_STATE : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001
            // Legacy flag, should not be used.
            // ES_USER_PRESENT = 0x00000004
        }
    }
}
