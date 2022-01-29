using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using KFlearning.App.Resources;
using KFlearning.App.Services;
using KFlearning.App.Views;
using KFlearning.Core.API;
using KFlearning.Core.Helpers;
using KFlearning.Core.Services;
using KFlearning.TemplateProvider;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace KFlearning.App
{
    internal static class Program
    {
        private const int MutexTimeout = 1000;
        private const string MutexName = "KFlearning.SingleInstanceGuard";

        public static ServiceProvider Container { get; private set; } = null!;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                // mutex to prevent multiple instance of application running
                using var mutex = new Mutex(true, MutexName);
                if (!mutex.WaitOne(MutexTimeout))
                {
                    MessageBox.Show(MessagesText.SingleInstanceError, MessagesText.AppName,
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // install services
                RegisterLogger();
                RegisterServices();

                // find vscode
                var path = Container.GetRequiredService<IPathManager>();
                if (!path.IsVscodeInstalled)
                {
                    Log.Debug("Visual Studio Code not found");
                    MessageBox.Show(MessagesText.VSCodeNotInstalled, MessagesText.AppName,
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // find mingw
                if (!path.IsKfMingwInstalled)
                {
                    Log.Debug("KF-MinGW not found");
                    MessageBox.Show(MessagesText.KFmingwNotInstalled, MessagesText.AppName,
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //return;
                }

                // enable TLS
                Log.Debug("Enabling TLS support");
                NetworkHelpers.EnableTls();

                // bootstrapper
                Log.Debug("Bootstrapping application");
                ApplicationConfiguration.Initialize();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(Container.GetRequiredService<KFlearningApplicationContext>());
            }
            catch (Exception e)
            {
                Log.Fatal("Application shutdown unexpectedly", e);
                Log.CloseAndFlush();

                MessageBox.Show(MessagesText.FatalShutdown, MessagesText.AppName,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                AppExitHandler();
            }
        }

        private static void RegisterLogger()
        {
            var logPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            logPath = Path.Combine(logPath, "KFlearning", "logs", "kflearning-.txt");
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(logPath, rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        private static void RegisterServices()
        {
            var services = new ServiceCollection();

            // register views and presenters
            services.AddTransient<StartupView>();
            services.AddTransient<StartupViewPresenter>();
            services.AddTransient<AboutView>();
            services.AddTransient<CreateProjectView>();
            services.AddTransient<CreateProjectViewPresenter>();
            services.AddTransient<FlutterInstallView>();
            services.AddTransient<FlutterInstallViewPresenter>();

            // register services
            services.AddSingleton<KFlearningApplicationContext>();
            services.AddSingleton<IHistoryService, HistoryService>();

            services.AddSingleton<IPathManager, PathManager>();
            services.AddSingleton<IProcessManager, ProcessManager>();
            services.AddSingleton<IProjectService, ProjectService>();
            services.AddSingleton<ITemplateService, TemplateService>();
            services.AddSingleton<IPersistanceStorage, PersistanceStorage>();
            services.AddSingleton<IFlutterInstallService, FlutterInstallService>();
            services.AddSingleton<IFlutterGitClient, FlutterGitClient>();
            services.AddSingleton<IVisualStudioCodeService, VisualStudioCodeService>();

            services.AddTransient<ITemplateProvider, CppConsoleProvider>();
            services.AddTransient<ITemplateProvider, CppFreeglutProvider>();
            services.AddTransient<ITemplateProvider, PythonProvider>();
            services.AddTransient<ITemplateProvider, WebProvider>();

            services.AddTransient<IUsesPersistence, HistoryService>();

            // register clients
            services.AddTransient<WebClient>();
            services.AddTransient<ManualResetEventSlim>();

            // setup logging
            services.AddLogging(configure =>
            {
                configure.AddSerilog();
            });

            Container = services.BuildServiceProvider();
        }

        private static void AppExitHandler()
        {
            try
            {
                foreach (var usesPersistence in Container.GetServices<IUsesPersistence>())
                {
                    usesPersistence.Save();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Cannot save persistence", ex);
            }

            Container.Dispose();
        }
    }
}
