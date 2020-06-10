using KFmaintenance.Views;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace KFmaintenance.Services
{
    public interface IFormService : IDisposable
    {
        void ShowClis();
        void ShowFileServer();
    }

    public class FormService : IFormService
    {
        private const int FileFormId = 0;
        private const int ClisFormId = 1;
        private Dictionary<int, Form> _forms = new Dictionary<int, Form>();

        public void ShowFileServer()
        {
            InternalShowForm(FileFormId, () => new FileServerForm());
        }

        public void ShowClis()
        {
            InternalShowForm(ClisFormId, () => new ClisForm());
        }

        private void InternalShowForm(int id, Func<Form> createFunc)
        {
            var formExists = _forms.TryGetValue(id, out Form form);
            if (!formExists || form == null || form.IsDisposed)
            {
                form = createFunc();
                _forms[id] = form;
            }

            form.Show();
        }

        #region IDisposable Support

        private bool _disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    foreach (var form in _forms.Values)
                    {
                        if (form != null && !form.IsDisposed) form.Close();
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                _disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~FormService()
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
