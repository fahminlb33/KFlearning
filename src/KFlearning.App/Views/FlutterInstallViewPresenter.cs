using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using KFlearning.Annotations;
using KFlearning.App.Services;

namespace KFlearning.App.Views
{
    public class FlutterInstallViewPresenter : INotifyPropertyChanged
    {
        public delegate void FormInvokceDelegate(Delegate action, params object[] args);

        private readonly IFlutterInstallService _installService;

        private string _cmdInstallText = "Install Flutter";
        private bool _cmdInstallEnabled;
        private bool _cmdBrowseEnabled;
        private string _txtInstallPathText = string.Empty;
        private string _lblStatusText = "Menunggu versi Flutter...";
        private string _lblFlutterVersionText = "...";
        private string _lblPercentText = "0%";
        private int _prgProgressValue;

        public FlutterInstallViewPresenter(IFlutterInstallService installService)
        {
            _installService = installService;

            _installService.InstallReady += InstallService_InstallReady;
            _installService.ProgressChanged += InstallService_ProgressChanged;
            _installService.InstallFinished += InstallService_InstallFinished;
        }

        #region Properties

        public Action? FormCloseAction { get; set; }
        public FormInvokceDelegate? FormInvokeAction { get; set; }
        public Func<bool> InvokeRequiredAction { get; set; } = () => false;

        public string CmdInstallText
        {
            get => _cmdInstallText;
            set
            {
                if (value == _cmdInstallText) return;
                _cmdInstallText = value;
                OnPropertyChanged();
            }
        }

        public bool CmdInstallEnabled
        {
            get => _cmdInstallEnabled;
            set
            {
                if (value == _cmdInstallEnabled) return;
                _cmdInstallEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool CmdBrowseEnabled
        {
            get => _cmdBrowseEnabled;
            set
            {
                if (value == _cmdBrowseEnabled) return;
                _cmdBrowseEnabled = value;
                OnPropertyChanged();
            }
        }

        public string TxtInstallPathText
        {
            get => _txtInstallPathText;
            set
            {
                if (value == _txtInstallPathText) return;
                _txtInstallPathText = value;
                OnPropertyChanged();
            }
        }

        public string LblStatusText
        {
            get => _lblStatusText;
            set
            {
                if (value == _lblStatusText) return;
                _lblStatusText = value;
                OnPropertyChanged();
            }
        }

        public string LblFlutterVersionText
        {
            get => _lblFlutterVersionText;
            set
            {
                if (value == _lblFlutterVersionText) return;
                _lblFlutterVersionText = value;
                OnPropertyChanged();
            }
        }

        public string LblPercentText
        {
            get => _lblPercentText;
            set
            {
                if (value == _lblPercentText) return;
                _lblPercentText = value;
                OnPropertyChanged();
            }
        }

        public int PrgProgressValue
        {
            get => _prgProgressValue;
            set
            {
                if (value == _prgProgressValue) return;
                _prgProgressValue = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region FlutterInstallService Event Handlers

        private void InstallService_InstallReady(object? sender, FlutterInstallReadyEventArgs e)
        {
            if (InvokeRequiredAction())
            {
                FormInvokeAction?.Invoke(new Action<object, FlutterInstallReadyEventArgs>(InstallService_InstallReady), sender, e);
            }
            else
            {
                CmdInstallEnabled = e.Ready;
                CmdBrowseEnabled = e.Ready;

                TxtInstallPathText = _installService.InstallPath;
                LblStatusText = e.Ready ? "Siap." : e.ErrorMessage;
                LblFlutterVersionText = e.Ready 
                    ? _installService.FlutterVersion 
                    : "Tidak diketahui.";
            }
        }

        private void InstallService_ProgressChanged(object? sender, FlutterInstallProgressEventArgs e)
        {
            if (InvokeRequiredAction())
            {
                FormInvokeAction?.Invoke(new Action<object, FlutterInstallProgressEventArgs>(InstallService_ProgressChanged), sender, e);
            }
            else
            {
                LblStatusText = e.Status;
                LblPercentText = $"{e.ProgressPercentage}%";
                PrgProgressValue = e.ProgressPercentage;
            }
        }

        private void InstallService_InstallFinished(object? sender, FlutterInstallFinishedEventArgs e)
        {
            if (InvokeRequiredAction())
            {
                FormInvokeAction?.Invoke(new Action<object, FlutterInstallFinishedEventArgs>(InstallService_InstallFinished), sender, e);
            }
            else
            {
                CmdBrowseEnabled = true;
                CmdInstallEnabled = true;
                CmdInstallText = "Tutup";

                PrgProgressValue = 0;
                LblPercentText = "0%";
                LblStatusText = e.ErrorMessage;
            }
        }

        #endregion

        public void OnLoadHandler()
        {
            _installService.PreparationStep();
        }

        public void CmdInstallClickHandler()
        {
            switch (CmdInstallText)
            {
                case "Install Flutter":
                    CmdBrowseEnabled = false;
                    CmdInstallText = "Batal";

                    _installService.InstallPath = TxtInstallPathText;
                    _installService.Install();
                    break;

                case "Tutup":
                    FormCloseAction?.Invoke();
                    break;

                default:
                    CmdInstallEnabled = false;
                    CmdInstallText = "Install Flutter";
                    _installService.Cancel();
                    break;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
