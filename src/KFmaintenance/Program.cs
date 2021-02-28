using System;
using System.Threading;
using System.Windows.Forms;
using Castle.Windsor;
using KFlearning.Core.Extensions;
using KFlearning.Core.Services;
using KFmaintenance.Properties;
using KFmaintenance.Services;

namespace KFmaintenance
{
    static class Program
    {
        private const int MutexTimeout = 1000;
        private const string MutexName = "KFmaintenance.SingleInstanceGuard";
        public static WindsorContainer Container = new WindsorContainer();

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var mutex = new Mutex(true, MutexName))
            {
                if (!mutex.WaitOne(MutexTimeout))
                {
                    MessageBox.Show(Resources.SingleInstanceMessage, Resources.AppName,
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // install services
                Container.Install(new KFmaintenanceModulesInstaller());

                // enable TLS;
                ApiHelpers.EnableTls();

                // app exit handler
                Application.ApplicationExit += Application_ApplicationExit;

                // bootstrap
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(Container.Resolve<KFmaintenanceApplicationContext>());
            }
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            try
            {
                foreach (var usesPersistance in Container.ResolveAll<IUsesPersistance>())
                {
                    usesPersistance.Save();
                }
            }
            catch
            {
                // ignore
            }

            Container.Dispose();
        }
    }
}