using KFlearning.Core.API;
using KFlearning.Core.Services;
using KFmaintenance.Properties;
using System;
using System.Threading.Tasks;

namespace KFmaintenance.Services
{
    public interface ITelemetryService : IUsesPersistance
    {
    }

    public class TelemetryService : ITelemetryService
    {
        private readonly ISystemInfoService _infoService;
        private readonly ITelemetryClient _telemetry;

        public TelemetryService(ITelemetryClient telemetry, ISystemInfoService infoService)
        {
            _telemetry = telemetry;
            _infoService = infoService;
        }

        public void Load()
        {
            try
            {
                _infoService.Query();
                Task.WaitAll(_telemetry.SendAppStart(Resources.AppName, _infoService.DeviceId),
                    _telemetry.SendIdentification(_infoService.DeviceId, _infoService.CPU, _infoService.RAM,
                        _infoService.OS, _infoService.Architecture));
            }
            catch (Exception)
            {
                // ignore
            }
        }

        public void Save()
        {
            try
            {
                _infoService.Query();
                Task.WaitAll(_telemetry.SendAppExit(Resources.AppName, _infoService.DeviceId));
            }
            catch (Exception)
            {
                // ignore
            }
        }
    }
}
