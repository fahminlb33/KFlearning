using System;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using KFlearning.App.Resources;
using KFlearning.App.Services;
using KFlearning.App.Views;
using KFlearning.Core.Extensions;
using KFlearning.Core.Services;
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
            //try
            //{
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
                    return;
                }

                // enable TLS
                Log.Debug("Enabling TLS support");
                NetworkHelpers.EnableTls();

                // app exit handler
                Application.ApplicationExit += Application_ApplicationExit;

                // bootstrapper
                Log.Debug("Bootstrapping application");

                ApplicationConfiguration.Initialize();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(Container.GetRequiredService<KFlearningApplicationContext>());
            //}
            //catch (Exception e)
            //{
            //    Log.Fatal("Application shutdown unexpectedly", e);
            //    Log.CloseAndFlush();

            //    MessageBox.Show(MessagesText.FatalShutdown, MessagesText.AppName,
            //        MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private static void RegisterLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("logs/kflearning-.txt", rollingInterval: RollingInterval.Day)
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
            services.AddSingleton<IVisualStudioCodeService, VisualStudioCodeService>();

            // register clients
            services.AddTransient<WebClient>();

            // setup logging
            services.AddLogging(configure =>
            {
                configure.AddSerilog();
            });

            Container = services.BuildServiceProvider();
        }

        private static void Application_ApplicationExit(object? sender, EventArgs e)
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
