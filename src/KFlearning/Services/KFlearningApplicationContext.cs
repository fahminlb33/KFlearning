using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Castle.Core.Logging;
using KFlearning.Views;

namespace KFlearning.Services
{
    public class KFlearningApplicationContext : ApplicationContext
    {
        private readonly ILogger _logger;
        private readonly Form _mainForm;

        public KFlearningApplicationContext(ITelemetryService telemetryService, StartupForm form, ILogger logger)
        {
            _mainForm = form;
            _logger = logger;

            _logger.Info("Sending telemetry");
            Task.Run(() => telemetryService.Load());

            _mainForm.HandleDestroyed += OnFormDestroy;
            _mainForm.Show();
        }

        private void OnFormDestroy(object sender, EventArgs e)
        {
            if (!(sender is Form form) || form.RecreatingHandle)
            {
                return;
            }

            _logger.Info("Form is being destroyed");
            form.HandleDestroyed -= OnFormDestroy;
            OnMainFormClosed(sender, e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _mainForm?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}