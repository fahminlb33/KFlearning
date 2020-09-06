// SOLUTION : KFlearning
// PROJECT  : KFlearning
// FILENAME : TelemetryService.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

using KFlearning.Core.API;
using KFlearning.Core.Services;
using KFlearning.Properties;
using System;
using System.Threading.Tasks;

namespace KFlearning.Services
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