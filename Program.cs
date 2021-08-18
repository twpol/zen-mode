using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace Zen_Mode
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var options = args.Where(arg => arg.StartsWith("/") || arg.StartsWith("-")).Select(arg => arg.Substring(1).ToLowerInvariant());
            if (options.Contains("start"))
            {
                StartBlockScreens();
            }
            else if (options.Contains("stop"))
            {
                StopBlockScreens();
            }
            else if (GetOtherProcesses().Any())
            {
                StopBlockScreens();
            }
            else
            {
                StartBlockScreens();
            }
        }

        static void StartBlockScreens()
        {
            var mouseScreen = Screen.FromPoint(Cursor.Position);
            foreach (var screen in Screen.AllScreens)
            {
                if (screen.DeviceName != mouseScreen.DeviceName)
                {
                    var block = new BlockScreen(screen);
                    block.FormClosed += (e, o) => Application.Exit();
                }
            }
            Application.Run();
        }

        static void StopBlockScreens()
        {
            foreach (var process in GetOtherProcesses())
            {
                process.Kill();
            }
        }

        static IEnumerable<Process> GetOtherProcesses()
        {
            var currentId = Process.GetCurrentProcess().Id;
            return Process.GetProcessesByName("Zen Mode").Where(process => process.Id != currentId);
        }
    }
}
