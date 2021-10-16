using System;
using System.Windows.Forms;
using KFlearning.App.Views;
using Microsoft.Extensions.Logging;

namespace KFlearning.App.Services
{
    public class KFlearningApplicationContext : ApplicationContext
    {
        private readonly ILogger<KFlearningApplicationContext> _logger;
        private readonly Form _mainForm;

        public KFlearningApplicationContext(StartupView form, ILogger<KFlearningApplicationContext> logger)
        {
            _mainForm = form;
            _logger = logger;

            _mainForm.HandleDestroyed += OnFormDestroy;
            _mainForm.Show();
        }

        private void OnFormDestroy(object? sender, EventArgs e)
        {
            if (sender is not Form form || form.RecreatingHandle)
            {
                return;
            }

            _logger.LogInformation("Form is being destroyed");
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