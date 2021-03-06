using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Ionic.Zip;
using KFlearning.Core.API;
using KFlearning.Core.Services;

namespace KFlearning.Services
{
    public interface IFlutterInstallService
    {
        string FlutterVersion { get; }
        string InstallPath { get; set; }

        event EventHandler<FlutterInstallReadyEventArgs> InstallReady;
        event EventHandler<FlutterInstallProgressEventArgs> ProgressChanged;
        event EventHandler<FlutterInstallFinishedEventArgs> InstallFinished;

        void Cancel();
        void Install();
        void PreparationStep();
    }

    public class FlutterInstallService : IFlutterInstallService
    {
        private readonly WebClient _webClient;
        private readonly IFlutterGitClient _flutterService;
        private readonly ILogger _logger;

        private CancellationTokenSource _cancellationSource;
        private string _downloadPath = "";

        public event EventHandler<FlutterInstallReadyEventArgs> InstallReady;
        public event EventHandler<FlutterInstallProgressEventArgs> ProgressChanged;
        public event EventHandler<FlutterInstallFinishedEventArgs> InstallFinished;

        public string FlutterVersion { get; private set; }
        public string InstallPath { get; set; }

        public FlutterInstallService(IFlutterGitClient flutterService, WebClient webClient, IPathManager pathManager,
            ILogger logger)
        {
            InstallPath = pathManager.GetPath(PathKind.FlutterInstallDirectory);

            _flutterService = flutterService;
            _webClient = webClient;
            _logger = logger;
            _webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
        }

        #region Public Methods

        public void Install()
        {
            _cancellationSource = new CancellationTokenSource();
            ExecuteSteps();
        }

        public void Cancel()
        {
            _webClient.CancelAsync();
            _cancellationSource.Cancel();
        }

        public void PreparationStep()
        {
            Task.Run(async () =>
            {
                try
                {
                    FlutterVersion = await _flutterService.GetLatestFlutterVersion();
                    _logger.DebugFormat("Flutter version is {0}", FlutterVersion);
                    OnInstallReady(this, new FlutterInstallReadyEventArgs
                    {
                        Ready = true
                    });
                }
                catch (Exception ex)
                {
                    _logger.Error("Can't check for Flutter version, fallback to default", ex);
                    FlutterVersion = FlutterGitClient.DefaultFlutterVersion;
                    OnInstallReady(this, new FlutterInstallReadyEventArgs
                    {
                        Ready = true,
                        ErrorMessage = "Menggunakan versi Flutter default."
                    });
                }
            });
        }

        #endregion

        #region Install Steps

        private void ExecuteSteps()
        {
            Task.Run(async () =>
            {
                try
                {
                    // step 1 --- download Flutter SDK
                    OnProgressChanged(this, new FlutterInstallProgressEventArgs
                    {
                        ProgressPercentage = 0,
                        Status = "Memulai unduhan..."
                    });

                    _downloadPath = Path.GetTempFileName();
                    var uri = _flutterService.GetFlutterDownloadUri(FlutterVersion);
                    _logger.DebugFormat("Flutter download path {0}", _downloadPath);
                    _logger.DebugFormat("Flutter URI path {0}", uri);

                    _logger.Info("Start Flutter download");
                    _cancellationSource.Token.Register(() => _webClient.CancelAsync());
                    await _webClient.DownloadFileTaskAsync(new Uri(uri), _downloadPath);

                    // step 2 --- extract to installfolder
                    OnProgressChanged(this, new FlutterInstallProgressEventArgs
                    {
                        ProgressPercentage = 0,
                        Status = "Memulai ekstraksi..."
                    });

                    _logger.Info("Start Flutter extraction");
                    _cancellationSource.Token.ThrowIfCancellationRequested();
                    using (var zip = new ZipFile(_downloadPath))
                    {
                        var totalEntries = zip.Entries.Count;
                        var processedEntry = 0;
                        foreach (var entry in zip) // non-parallelism, the lib doesn't support parallel
                        {
                            _cancellationSource.Token.ThrowIfCancellationRequested();

                            var path = Path.GetFullPath(Path.Combine(InstallPath, "../" + entry.FileName));
                            if (entry.IsDirectory)
                            {
                                Directory.CreateDirectory(path);
                            }
                            else
                            {
                                using (var fs = new FileStream(path, FileMode.Create))
                                {
                                    entry.Extract(fs);
                                }
                            }

                            Interlocked.Increment(ref processedEntry);
                            OnProgressChanged(this, new FlutterInstallProgressEventArgs
                            {
                                ProgressPercentage = (int) ((double) processedEntry / totalEntries * 100),
                                Status = "Memindahkan direktori..."
                            });
                        }
                    }

                    // step 3 --- set environment variable
                    OnProgressChanged(this, new FlutterInstallProgressEventArgs
                    {
                        ProgressPercentage = 70,
                        Status = "Mengatur environment variable..."
                    });

                    _logger.Info("Start Flutter environment configuration");
                    _cancellationSource.Token.ThrowIfCancellationRequested();
                    var pathEnv = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.User);
                    if (pathEnv?.Contains(@"flutter\bin") == false)
                    {
                        pathEnv += ";" + Path.Combine(InstallPath, @"bin");
                        Environment.SetEnvironmentVariable("PATH", pathEnv, EnvironmentVariableTarget.User);
                    }

                    // --- all done!
                    _logger.Info("Flutter install sequence finished");
                    OnInstallFinished(this, new FlutterInstallFinishedEventArgs
                    {
                        ErrorMessage = "Instalasi selesai.",
                        Success = true
                    });
                }
                catch (TaskCanceledException cancelledEx)
                {
                    _logger.Info("Flutter install sequence cancelled.", cancelledEx);
                    OnInstallFinished(this, new FlutterInstallFinishedEventArgs
                    {
                        ErrorMessage = "Instalasi dibatalkan.",
                        Success = false
                    });
                }
                catch (Exception ex)
                {
                    _logger.Error("Flutter install sequence failed", ex);
                    OnInstallFinished(this, new FlutterInstallFinishedEventArgs
                    {
                        ErrorMessage = ex.Message,
                        Success = false
                    });
                }
            });
        }

        #endregion

        #region Event Handlers

        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            OnProgressChanged(this, new FlutterInstallProgressEventArgs
            {
                ProgressPercentage = e.ProgressPercentage,
                Status = "Mengunduh..."
            });
        }

        #endregion

        #region Event Invocators

        private void OnInstallReady(object sender, FlutterInstallReadyEventArgs e)
        {
            InstallReady?.Invoke(sender, e);
        }

        private void OnProgressChanged(object sender, FlutterInstallProgressEventArgs e)
        {
            ProgressChanged?.Invoke(sender, e);
        }

        private void OnInstallFinished(object sender, FlutterInstallFinishedEventArgs e)
        {
            InstallFinished?.Invoke(sender, e);
        }

        #endregion
    }

    public class FlutterInstallReadyEventArgs : EventArgs
    {
        public bool Ready { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class FlutterInstallProgressEventArgs : EventArgs
    {
        public int ProgressPercentage { get; set; }
        public string Status { get; set; }
    }

    public class FlutterInstallFinishedEventArgs : EventArgs
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}