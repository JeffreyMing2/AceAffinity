using AceAffinity.Core.ProcessManager;
using AceAffinity.Core.SchedulerEngine;
using AceAffinity.Core.WinApiWrapper;
using System.Diagnostics;

namespace AceAffinity.Application
{
    /// <summary>
    /// Acts as the central controller for the application's business logic.
    /// It coordinates the ProcessService and SchedulerService to apply optimizations.
    /// </summary>
    public class MainController
    {
        private readonly ProcessService _processService;
        private readonly SchedulerService _schedulerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainController"/> class.
        /// </summary>
        public MainController()
        {
            _processService = new ProcessService();
            _schedulerService = new SchedulerService();
        }

        /// <summary>
        /// Finds a process by name and applies CPU affinity and priority optimizations.
        /// </summary>
        /// <param name="processName">The name of the process to optimize.</param>
        /// <param name="priority">The priority to set for the process. Defaults to Idle.</param>
        /// <returns>True if the process was found and optimizations were applied successfully; otherwise, false.</returns>
        public bool OptimizeProcess(string processName, NativeMethods.PriorityClass priority = NativeMethods.PriorityClass.IDLE_PRIORITY_CLASS)
        {
            if (string.IsNullOrWhiteSpace(processName))
            {
                Debug.WriteLine("Controller: Process name cannot be empty.");
                return false;
            }

            Debug.WriteLine($"Controller: Attempting to find process '{processName}'...");
            Process? targetProcess = _processService.FindProcessByName(processName);

            if (targetProcess == null)
            {
                Debug.WriteLine($"Controller: Process '{processName}' not found.");
                return false;
            }

            Debug.WriteLine($"Controller: Process found with ID {targetProcess.Id}. Applying optimizations...");
            bool success = _schedulerService.ApplyAffinityAndPriority(targetProcess.Id, priority);

            if (success)
            {
                Debug.WriteLine("Controller: Successfully applied optimizations.");
            }
            else
            {
                Debug.WriteLine("Controller: Failed to apply optimizations.");
            }

            // Dispose the process object to release resources.
            targetProcess.Dispose();
            return success;
        }
    }
}
