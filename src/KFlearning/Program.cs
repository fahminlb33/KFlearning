// SOLUTION : KFlearning
// PROJECT  : KFlearning
// FILENAME : Program.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

using System;
using System.Threading;
using System.Windows.Forms;
using Castle.Windsor;
using KFlearning.Core;
using KFlearning.Core.Services;
using KFlearning.Properties;
using KFlearning.Services;
using KFlearning.Views;

namespace KFlearning
{
    static class Program
    {
        private const int MutexTimeout = 1000;
        private const string MutexName = "KFlearning.SingleInstanceGuard";
        public static WindsorContainer Container = new WindsorContainer();

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
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
                Container.Install(new AppModulesInstaller());

                // find vscode
                var path = Container.Resolve<IPathManager>();
                if (!path.IsVscodeInstalled())
                {
                    MessageBox.Show(Resources.VscodeNotInstalled, Resources.AppName, MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    return;
                }

                // find mingw
                if (!path.IsKfMingwInstalled())
                {
                    MessageBox.Show(Resources.KfmingwNotInstalled, Resources.AppName, MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    return;
                }

                // enable TLS
                Helpers.EnableTls();

                // app exit handler
                Application.ApplicationExit += Application_ApplicationExit;

                // bootstrapper
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(Container.Resolve<CustomApplicationContext>());
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