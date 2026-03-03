using AceAffinity.Core.WinApiWrapper;
using System.ComponentModel;
using System.Diagnostics;

namespace AceAffinity.Core.SchedulerEngine
{
    public class SchedulerService
    {
        public bool ApplyAffinityAndPriority(int processId, NativeMethods.PriorityClass priority)
        {
            IntPtr processHandle = IntPtr.Zero;
            try
            {
                processHandle = NativeMethods.OpenProcess(
                    NativeMethods.ProcessAccessFlags.PROCESS_SET_INFORMATION | NativeMethods.ProcessAccessFlags.PROCESS_QUERY_INFORMATION,
                    false,
                    processId);

                if (processHandle == IntPtr.Zero)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error(), $"Failed to open process with ID: {processId}");
                }

                // Set Affinity to the last logical core
                int coreCount = Environment.ProcessorCount;
                if (coreCount > 0)
                {
                    IntPtr affinityMask = (IntPtr)(1UL << (coreCount - 1));
                    if (!NativeMethods.SetProcessAffinityMask(processHandle, affinityMask))
                    {
                        throw new Win32Exception(Marshal.GetLastWin32Error(), "Failed to set process affinity.");
                    }
                }

                // Set Priority
                if (!NativeMethods.SetPriorityClass(processHandle, priority))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error(), "Failed to set process priority.");
                }

                return true;
            }
            catch (Exception ex)
            {
                // In a real application, you'd want a proper logging mechanism here.
                Debug.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
            finally
            {
                if (processHandle != IntPtr.Zero)
                {
                    NativeMethods.CloseHandle(processHandle);
                }
            }
        }
    }
}
