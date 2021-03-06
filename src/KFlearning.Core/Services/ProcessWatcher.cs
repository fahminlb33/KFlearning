using System;
using System.Diagnostics;
using System.Linq;
using System.Timers;

namespace KFlearning.Core.Services
{
    public interface IProcessWatcher
    {
        string ProcessName { get; set; }
        double TotalSeconds { get; set; }

        void Start();
        void Stop();
    }

    public class ProcessWatcher : IProcessWatcher
    {
        private const double PollingInterval = 5 * 60 * 1000;

        private readonly object _lock = new object();
        private readonly Timer _timer;
        private DateTime _lastCheck;

        public string ProcessName { get; set; }
        public double TotalSeconds { get; set; }

        public ProcessWatcher()
        {
            _timer = new Timer {Enabled = false, AutoReset = true, Interval = PollingInterval};
            _timer.Elapsed += _timer_Elapsed;
        }

        public void Start()
        {
            if (_timer.Enabled) return;

            TotalSeconds = 0;
            _lastCheck = DateTime.Now;
            _timer.Start();
        }

        public void Stop()
        {
            if (!_timer.Enabled) return;

            _timer_Elapsed(null, null);
            _timer.Stop();
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                var processes = Process.GetProcessesByName(ProcessName);
                if (!processes.Any()) return;
                var now = DateTime.Now;

                lock (_lock)
                {
                    TotalSeconds += (now - _lastCheck).TotalSeconds;
                    _lastCheck = now;
                }
            }
            catch
            {
                // ignored
            }
        }
    }
}