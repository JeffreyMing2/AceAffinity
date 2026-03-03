using System.Runtime.InteropServices;

namespace AceAffinity.Core.WinApiWrapper
{
    /// <summary>
    /// Provides P/Invoke wrappers for native Windows API functions.
    /// This class uses LibraryImport for better performance and is internal to the core assembly.
    /// </summary>
    public static partial class NativeMethods
    {
        /// <summary>
        /// Defines the access rights for opening a process object.
        /// </summary>
        [Flags]
        public enum ProcessAccessFlags : uint
        {
            PROCESS_VM_OPERATION = 0x0008,
            PROCESS_VM_READ = 0x0010,
            PROCESS_VM_WRITE = 0x0020,
            PROCESS_QUERY_INFORMATION = 0x0400,
            PROCESS_SET_INFORMATION = 0x0200,
            SYNCHRONIZE = 0x00100000,
            PROCESS_ALL_ACCESS = 0x001F0FFF
        }

        /// <summary>
        /// Defines the priority classes for a process.
        /// </summary>
        public enum PriorityClass : uint
        {
            IDLE_PRIORITY_CLASS = 64,
            BELOW_NORMAL_PRIORITY_CLASS = 16384,
            NORMAL_PRIORITY_CLASS = 32,
            ABOVE_NORMAL_PRIORITY_CLASS = 32768,
            HIGH_PRIORITY_CLASS = 128,
            REALTIME_PRIORITY_CLASS = 256
        }

        /// <summary>
        /// Opens an existing local process object.
        /// </summary>
        /// <param name="processAccess">The access to the process object. This access right is checked against the security descriptor for the process.</param>
        /// <param name="bInheritHandle">If this value is TRUE, processes created by this process will inherit the handle. Otherwise, the processes do not inherit this handle.</param>
        /// <param name="processId">The identifier of the local process to be opened.</param>
        /// <returns>If the function succeeds, the return value is an open handle to the specified process. If the function fails, the return value is IntPtr.Zero.</returns>
        [LibraryImport("kernel32.dll", SetLastError = true)]
        internal static partial IntPtr OpenProcess(
            ProcessAccessFlags processAccess,
            [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle,
            int processId);

        /// <summary>
        /// Sets a processor affinity mask for the threads of a specified process.
        /// </summary>
        /// <param name="hProcess">A handle to the process whose affinity mask is to be set.</param>
        /// <param name="dwProcessAffinityMask">The affinity mask for the threads of the process.</param>
        /// <returns>If the function succeeds, the return value is true. If the function fails, the return value is false.</returns>
        [LibraryImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool SetProcessAffinityMask(
            IntPtr hProcess,
            IntPtr dwProcessAffinityMask);

        /// <summary>
        /// Sets the priority class for the specified process.
        /// </summary>
        /// <param name="hProcess">A handle to the process.</param>
        /// <param name="dwPriorityClass">The priority class for the process.</param>
        /// <returns>If the function succeeds, the return value is true. If the function fails, the return value is false.</returns>
        [LibraryImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool SetPriorityClass(
            IntPtr hProcess,
            PriorityClass dwPriorityClass);

        /// <summary>
        /// Closes an open object handle.
        /// </summary>
        /// <param name="hObject">A valid handle to an open object.</param>
        /// <returns>If the function succeeds, the return value is true. If the function fails, the return value is false.</returns>
        [LibraryImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool CloseHandle(IntPtr hObject);
    }
}
