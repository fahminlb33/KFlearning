using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;

namespace KFlearning.Core.CLIS
{
    public class ClisProgressEventArgs : EventArgs
    {
        public int ProgressPercentage { get; set; }
        public string Message { get; set; }
        public string Step { get; set; }
    }

    public interface IClisService : IDisposable
    {
        bool ProcessFinalScore { get; set; }
        bool UseHeadless { get; set; }
        bool ProcessLecturerStudent { get; set; }
        bool ProcessMasterStudent { get; set; }
        string WebDriverPath { get; set; }

        event EventHandler<ClisProgressEventArgs> ProcessFinished;
        event EventHandler ProcessStarted;
        event EventHandler<ClisProgressEventArgs> ProgressChanged;

        void Start(ClisMetadata metadata);
        void Stop();
    }

    public class ClisService : IClisService
    {
        #region Fields

        private Thread _workerThread;
        private CancellationTokenSource _cancellationTokenSource;
        private ConcurrentQueue<IClisActionStep> _steps = new ConcurrentQueue<IClisActionStep>();
        private ClisMetadata _metadata;

        #endregion

        #region Properties

        public string WebDriverPath { get; set; }
        public bool UseHeadless { get; set; }
        public bool ProcessMasterStudent { get; set; }
        public bool ProcessLecturerStudent { get; set; }
        public bool ProcessFinalScore { get; set; }

        #endregion


        #region Events

        public event EventHandler ProcessStarted;
        public event EventHandler<ClisProgressEventArgs> ProgressChanged;
        public event EventHandler<ClisProgressEventArgs> ProcessFinished;

        #endregion

        #region Public Methods

        public void Start(ClisMetadata metadata)
        {
            _metadata = metadata;
            _steps.Enqueue(new ClisAuthenticationStep());
            if (ProcessMasterStudent) _steps.Enqueue(new ClisMasterStudentStep());
            if (ProcessLecturerStudent) _steps.Enqueue(new ClisLecturerStudentStep());
            if (ProcessFinalScore) _steps.Enqueue(new ClisFinalScoreStep());

            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();
            _workerThread = new Thread(InternalWorkerCallback) { IsBackground = true };
            _workerThread.Start();
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();
        }

        #endregion

        #region Private Methods

        private void InternalWorkerCallback()
        {
            OnProcessStarted();
            try
            {
                var driverOptions = new ChromeOptions();
                var driverService = ChromeDriverService.CreateDefaultService(Path.GetDirectoryName(WebDriverPath));
                if (UseHeadless)
                {
                    driverOptions.AddArgument("headless");
                    driverService.HideCommandPromptWindow = true;
                }
                
                using (var driver = new ChromeDriver(driverService, driverOptions))
                {
                    while (!_steps.IsEmpty)
                    {
                        if (!_steps.TryDequeue(out IClisActionStep step)) continue;
                        _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                        var stepName = step.GetType().Name;
                        var progressDelegate = new Action<ClisProgress>(x => OnProgressChanged(new ClisProgressEventArgs
                        {
                            Message = x.Message,
                            ProgressPercentage = x.ProgressPercentage,
                            Step = stepName
                        }));

                        step.Fill(driver, _metadata, progressDelegate, _cancellationTokenSource.Token);
                    }
                }

                OnProcessFinished(new ClisProgressEventArgs
                {
                    Step = "Processor",
                    Message = "Process completed.",
                    ProgressPercentage = 100
                });
            }
            catch (OperationCanceledException)
            {
                OnProcessFinished(new ClisProgressEventArgs
                {
                    Step = "Processor",
                    Message = "Process cancelled.",
                    ProgressPercentage = 100
                });
            }
            catch (Exception ex)
            {
                OnProcessFinished(new ClisProgressEventArgs
                {
                    Step = "Processor",
                    Message = "Process has encoutered an error. " + ex.Message,
                    ProgressPercentage = 100
                });
            }
        }

        private void OnProcessStarted()
        {
            ProcessStarted?.Invoke(this, EventArgs.Empty);
        }

        #region Event Invocator

        private void OnProgressChanged(ClisProgressEventArgs e)
        {
            ProgressChanged?.Invoke(this, e);
        }

        private void OnProcessFinished(ClisProgressEventArgs e)
        {
            ProcessFinished?.Invoke(this, e);
        }

        #endregion

        #endregion

        #region IDisposable Support

        private bool disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _cancellationTokenSource?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ClisService()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
