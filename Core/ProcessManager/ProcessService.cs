using System.Diagnostics;
using System.IO;

namespace AceAffinity.Core.ProcessManager
{
    /// <summary>
    /// Provides services for finding and managing system processes.
    /// </summary>
    public class ProcessService
    {
        /// <summary>
        /// Finds the first running process that matches the given name.
        /// </summary>
        /// <param name="processName">The name of the process to find. This can be with or without the file extension.</param>
        /// <returns>A <see cref="Process"/> object if a matching process is found; otherwise, null.</returns>
        public Process? FindProcessByName(string processName)
        {
            if (string.IsNullOrWhiteSpace(processName))
            {
                return null;
            }

            // The GetProcessesByName method expects the process name without the extension.
            // We use Path.GetFileNameWithoutExtension to ensure compatibility whether the user provides "notepad" or "notepad.exe".
            string nameWithoutExtension = Path.GetFileNameWithoutExtension(processName);

            Process[] processes = Process.GetProcessesByName(nameWithoutExtension);

            if (processes.Length > 0)
            {
                // Return the first match for simplicity.
                // In a more advanced scenario, you might want to handle multiple instances (e.g., return a list or let the user choose).
                return processes[0];
            }

            return null;
        }
    }
}
