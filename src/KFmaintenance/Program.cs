// SOLUTION : KFlearning
// PROJECT  : KFmaintenance
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
using KFmaintenance.Properties;
using KFmaintenance.Services;
using KFmaintenance.Views;

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
                if (mutex.WaitOne(MutexTimeout))
                {
                    // install modules
                    Container.Install(new AppModulesInstaller());

                    // enable TLS
                    Helpers.EnableTls();

                    // bootstrap
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new StartupForm());
                }
                else
                {
                    MessageBox.Show(Resources.SingleInstanceMessage, Resources.AppName,
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
    }
}