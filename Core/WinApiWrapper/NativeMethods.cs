using System.Runtime.InteropServices;

namespace AceAffinity.Core.WinApiWrapper
{
    internal static partial class NativeMethods
    {
        [Flags]
        internal enum ProcessAccessFlags : uint
        {
            PROCESS_VM_OPERATION = 0x0008,
            PROCESS_VM_READ = 0x0010,
            PROCESS_VM_WRITE = 0x0020,
            PROCESS_QUERY_INFORMATION = 0x0400,
            PROCESS_SET_INFORMATION = 0x0200,
            SYNCHRONIZE = 0x00100000,
            PROCESS_ALL_ACCESS = 0x001F0FFF
        }

        internal enum PriorityClass : uint
        {
            IDLE_PRIORITY_CLASS = 64,
            BELOW_NORMAL_PRIORITY_CLASS = 16384,
            NORMAL_PRIORITY_CLASS = 32,
            ABOVE_NORMAL_PRIORITY_CLASS = 32768,
            HIGH_PRIORITY_CLASS = 128,
            REALTIME_PRIORITY_CLASS = 256
        }

        [LibraryImport("kernel32.dll", SetLastError = true)]
        internal static partial IntPtr OpenProcess(
            ProcessAccessFlags processAccess,
            [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle,
            int processId);

        [LibraryImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool SetProcessAffinityMask(
            IntPtr hProcess,
            IntPtr dwProcessAffinityMask);

        [LibraryImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool SetPriorityClass(
            IntPtr hProcess,
            PriorityClass dwPriorityClass);

        [LibraryImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool CloseHandle(IntPtr hObject);
    }
}
