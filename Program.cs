using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using SCUMServerListener.UI;

namespace SCUMServerListener
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static async Task Main()
        {
            Application.EnableVisualStyles();
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.SetCompatibleTextRenderingDefault(false);
        
            await RunAsync(new Gui());
        }

        private static Task RunAsync(Form form)
        {
            var tcs = new TaskCompletionSource<object?>();
        
            form.Load += (_, __) => tcs.SetResult(null);
            Application.Run(form);
        
            return tcs.Task;
        }
    }
}
