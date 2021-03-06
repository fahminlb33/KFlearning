using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using KFmaintenance.Views;

namespace KFmaintenance.Services
{
    public interface IFormService : IDisposable
    {
        void ShowFileServer();
    }

    public class FormService : IFormService
    {
        private const int FileFormId = 0;
        private readonly Dictionary<int, Form> _forms = new Dictionary<int, Form>();

        public void ShowFileServer()
        {
            InternalShowForm(FileFormId, () => new FileServerForm());
        }

        private void InternalShowForm(int id, Func<Form> createFunc)
        {
            var formExists = _forms.TryGetValue(id, out var form);
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
            if (_disposedValue)
            {
                return;
            }

            if (disposing)
            {
                foreach (var form in _forms.Values.Where(form => form != null && !form.IsDisposed))
                {
                    form.Close();
                }
            }
            
            _disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
