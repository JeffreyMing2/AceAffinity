using System.Diagnostics;

namespace AceAffinity.Core.ProcessManager
{
    public class ProcessService
    {
        public Process? FindProcessByName(string processName)
        {
            if (string.IsNullOrWhiteSpace(processName))
            {
                return null;
            }

            // The GetProcessesByName method expects the process name without the extension.
            string nameWithoutExtension = Path.GetFileNameWithoutExtension(processName);

            Process[] processes = Process.GetProcessesByName(nameWithoutExtension);

            if (processes.Length > 0)
            {
                // Return the first match for simplicity.
                // In a more advanced scenario, you might want to handle multiple instances.
                return processes[0];
            }

            return null;
        }
    }
}
