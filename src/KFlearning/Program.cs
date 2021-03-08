using System;
using System.Threading;
using System.Windows.Forms;
using Castle.Core.Logging;
using Castle.Windsor;
using KFlearning.Core.Extensions;
using KFlearning.Core.Services;
using KFlearning.Properties;
using KFlearning.Services;

namespace KFlearning
{
    static class Program
    {
        private const int MutexTimeout = 1000;
        private const string MutexName = "KFlearning.SingleInstanceGuard";

        public static WindsorContainer Container = new WindsorContainer();
        private static ILogger _logger;

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
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
                    NLog.GlobalDiagnosticsContext.Set("logDirectory", PathHelpers.GetLogPath());
                    Container.Install(new KFlearningModulesInstaller());
                    _logger = Container.Resolve<ILogger>();

                    // find vscode
                    var path = Container.Resolve<IPathManager>();
                    if (!path.IsVscodeInstalled)
                    {
                        _logger.Debug("Visual Studio Code not found");
                        MessageBox.Show(Resources.VscodeNotInstalled, Resources.AppName, MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                        return;
                    }

                    // find mingw
                    if (!path.IsKfMingwInstalled)
                    {
                        _logger.Debug("KF-MinGW not found");
                        MessageBox.Show(Resources.KfmingwNotInstalled, Resources.AppName, MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                        return;
                    }

                    // enable TLS
                    _logger.Debug("Enabling TLS support");
                    ApiHelpers.EnableTls();

                    // app exit handler
                    Application.ApplicationExit += Application_ApplicationExit;

                    // bootstrapper
                    _logger.Debug("Bootstrapping application");
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(Container.Resolve<KFlearningApplicationContext>());
                }
            }
            catch (Exception e)
            {
                _logger.Fatal("Application shutdown unexpectedly", e);
                MessageBox.Show("Aplikasi mengalami crash dan harus ditutup. Harap laporkan kepada asprak.",
                    Resources.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            try
            {
                foreach (var usesPersistence in Container.ResolveAll<IUsesPersistence>())
                {
                    usesPersistence.Save();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Cannot save persistence", ex);
            }

            Container.Dispose();
        }
    }
}