using AceAffinity.Core.WinApiWrapper;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AceAffinity.Core.SchedulerEngine
{
    /// <summary>
    /// Provides services for applying scheduling optimizations to processes.
    /// </summary>
    public class SchedulerService
    {
        /// <summary>
        /// Applies CPU affinity and priority settings to a specified process.
        /// </summary>
        /// <param name="processId">The ID of the process to optimize.</param>
        /// <param name="priority">The priority class to set for the process.</param>
        /// <returns>True if the optimizations were applied successfully; otherwise, false.</returns>
        public bool ApplyAffinityAndPriority(int processId, NativeMethods.PriorityClass priority)
        {
            IntPtr processHandle = IntPtr.Zero;
            try
            {
                // Open the process with required access rights for setting information.
                processHandle = NativeMethods.OpenProcess(
                    NativeMethods.ProcessAccessFlags.PROCESS_SET_INFORMATION | NativeMethods.ProcessAccessFlags.PROCESS_QUERY_INFORMATION,
                    false,
                    processId);

                if (processHandle == IntPtr.Zero)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error(), $"Failed to open process with ID: {processId}");
                }

                // Calculate the affinity mask for the last logical core.
                int coreCount = Environment.ProcessorCount;
                if (coreCount > 0)
                {
                    // The affinity mask is a bitmask. 1UL << (coreCount - 1) sets the bit for the last core.
                    IntPtr affinityMask = (IntPtr)(1UL << (coreCount - 1));
                    if (!NativeMethods.SetProcessAffinityMask(processHandle, affinityMask))
                    {
                        throw new Win32Exception(Marshal.GetLastWin32Error(), "Failed to set process affinity.");
                    }
                }

                // Set the process priority.
                if (!NativeMethods.SetPriorityClass(processHandle, priority))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error(), "Failed to set process priority.");
                }

                return true;
            }
            catch (Exception ex)
            {
                // In a real application, a more robust logging mechanism should be used.
                Debug.WriteLine($"An error occurred in SchedulerService: {ex.Message}");
                return false;
            }
            finally
            {
                // Always ensure the process handle is closed to prevent resource leaks.
                if (processHandle != IntPtr.Zero)
                {
                    NativeMethods.CloseHandle(processHandle);
                }
            }
        }
    }
}
