using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using KFlearning.Core.API;
using KFlearning.Core.Services;
using Microsoft.Extensions.Logging;

namespace KFlearning.App.Services
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
        private readonly IFlutterGitClient _flutterGitClient;
        private readonly ILogger<FlutterInstallService> _logger;

        private CancellationTokenSource? _cancellationSource;
        private CancellationToken _cancellationToken;
        private string _downloadPath = string.Empty;

        public event EventHandler<FlutterInstallReadyEventArgs>? InstallReady;
        public event EventHandler<FlutterInstallProgressEventArgs>? ProgressChanged;
        public event EventHandler<FlutterInstallFinishedEventArgs>? InstallFinished;

        public string FlutterVersion { get; private set; } = string.Empty;
        public string InstallPath { get; set; }

        public FlutterInstallService(IFlutterGitClient flutterGitClient, IPathManager pathManager, ILogger<FlutterInstallService> logger)
        {
            InstallPath = pathManager.GetPath(PathKind.FlutterInstallRoot);

            _flutterGitClient = flutterGitClient;
            _logger = logger;
        }

        #region Public Methods

        public void Install()
        {
            _cancellationSource = new CancellationTokenSource();
            _cancellationToken = _cancellationSource.Token;

            ExecuteSteps();
        }

        public void Cancel()
        {
            _cancellationSource?.Cancel();
        }

        public void PreparationStep()
        {
            Task.Run(async () =>
            {
                try
                {
                    FlutterVersion = await _flutterGitClient.GetLatestFlutterVersion();
                    _logger.LogDebug("Flutter version is {0}", FlutterVersion);
                    OnInstallReady(this, new FlutterInstallReadyEventArgs
                    {
                        Ready = true
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError("Can't check for Flutter version, fallback to default", ex);
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
                    var uri = _flutterGitClient.GetFlutterDownloadUri(FlutterVersion);
                    _logger.LogDebug("Flutter download URI {0}", uri);
                    _logger.LogDebug("Flutter download path {0}", _downloadPath);

                    _logger.LogInformation("Start Flutter download");

                    using var httpClient = new HttpClient();
                    using var response = await httpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead, _cancellationToken);
                    using var downloadStream = await response.Content.ReadAsStreamAsync(_cancellationToken);
                    using var fileWriterStream = new FileStream(_downloadPath, FileMode.Create);

                    var totalSize = response.Content.Headers.ContentLength ?? 1;
                    var totalDownloaded = 0D;
                    var buffer = new byte[8192];
                    var readed = 0;

                    do
                    {
                        readed = await downloadStream.ReadAsync(buffer, _cancellationToken);
                        if (readed == 0)
                        {
                            break;
                        }

                        await fileWriterStream.WriteAsync(buffer.AsMemory(0, readed), _cancellationToken);
                        totalDownloaded += readed;

                        var progressPercentage = Convert.ToInt32(totalDownloaded / totalSize * 100.0);

                        _logger.LogDebug("Download progress {0}/{1} ({2}%)", totalDownloaded, totalSize, progressPercentage);
                        OnProgressChanged(this, new FlutterInstallProgressEventArgs
                        {
                            ProgressPercentage = progressPercentage,
                            Status = "Mengunduh paket instalasi..."
                        });
                    } while (readed > 0);

                    // step 2 --- extract to installfolder
                    OnProgressChanged(this, new FlutterInstallProgressEventArgs
                    {
                        ProgressPercentage = 45,
                        Status = "Mengekstrak instalasi..."
                    });

                    _logger.LogInformation("Start Flutter extraction");
                    _cancellationSource?.Token.ThrowIfCancellationRequested();

                    ZipFile.ExtractToDirectory(_downloadPath, InstallPath);

                    // step 3 --- set environment variable
                    OnProgressChanged(this, new FlutterInstallProgressEventArgs
                    {
                        ProgressPercentage = 70,
                        Status = "Mengatur environment variable..."
                    });

                    _logger.LogInformation("Start Flutter environment configuration");
                    _cancellationToken.ThrowIfCancellationRequested();

                    var pathEnv = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.User);
                    if (pathEnv?.Contains(@"flutter\bin") == false)
                    {
                        pathEnv += ";" + Path.Combine(InstallPath, @"flutter\bin");
                        Debug.Print(pathEnv);
                        //Environment.SetEnvironmentVariable("PATH", pathEnv, EnvironmentVariableTarget.User);
                    }

                    // --- all done!
                    _logger.LogInformation("Flutter install sequence finished");
                    OnInstallFinished(this, new FlutterInstallFinishedEventArgs
                    {
                        ErrorMessage = "Instalasi selesai.",
                        Success = true
                    });
                }
                catch (TaskCanceledException cancelledEx)
                {
                    _logger.LogInformation("Flutter install sequence cancelled.", cancelledEx);
                    OnInstallFinished(this, new FlutterInstallFinishedEventArgs
                    {
                        ErrorMessage = "Instalasi dibatalkan.",
                        Success = false
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError("Flutter install sequence failed", ex);
                    OnInstallFinished(this, new FlutterInstallFinishedEventArgs
                    {
                        ErrorMessage = ex.Message,
                        Success = false
                    });
                }
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