using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace HFLive
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (Mutex mutex = new Mutex(false, "Global\\" + Assembly.GetExecutingAssembly().GetType().GUID.ToString()))
            {
                if (!mutex.WaitOne(0, false))
                {
                    return;
                }
                else
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new HFLive());
                }
            }
        }
    }
}
