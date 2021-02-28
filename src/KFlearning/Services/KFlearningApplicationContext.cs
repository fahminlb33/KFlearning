using KFlearning.Views;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KFlearning.Services
{
    public class KFlearningApplicationContext : ApplicationContext
    {
        private readonly Form _mainForm;

        public KFlearningApplicationContext(ITelemetryService telemetryService, StartupForm form)
        {
            _mainForm = form;

            Task.Run(() => telemetryService.Load());

            _mainForm.HandleDestroyed += OnFormDestroy;
            _mainForm.Show();
        }

        private void OnFormDestroy(object sender, EventArgs e)
        {
            if (!(sender is Form form) || form.RecreatingHandle) return;
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
