using AceAffinity.Core.ProcessManager;
using AceAffinity.Core.SchedulerEngine;
using AceAffinity.Core.WinApiWrapper;
using System.Diagnostics;

namespace AceAffinity.Application
{
    public class MainController
    {
        private readonly ProcessService _processService;
        private readonly SchedulerService _schedulerService;

        public MainController()
        {
            _processService = new ProcessService();
            _schedulerService = new SchedulerService();
        }

        public bool OptimizeProcess(string processName, NativeMethods.PriorityClass priority = NativeMethods.PriorityClass.IDLE_PRIORITY_CLASS)
        {
            if (string.IsNullOrWhiteSpace(processName))
            {
                Debug.WriteLine("Process name cannot be empty.");
                return false;
            }

            Debug.WriteLine($"Attempting to find process: {processName}");
            Process? targetProcess = _processService.FindProcessByName(processName);

            if (targetProcess == null)
            {
                Debug.WriteLine($"Process '{processName}' not found.");
                return false;
            }

            Debug.WriteLine($"Process found with ID: {targetProcess.Id}. Applying optimizations...");
            bool success = _schedulerService.ApplyAffinityAndPriority(targetProcess.Id, priority);

            if (success)
            {
                Debug.WriteLine("Successfully applied optimizations.");
            }
            else
            {
                Debug.WriteLine("Failed to apply optimizations.");
            }

            targetProcess.Dispose();
            return success;
        }
    }
}
