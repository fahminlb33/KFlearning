﻿using System;
using System.Threading.Tasks;
using Castle.Core.Logging;
using KFlearning.Core.API;
using KFlearning.Core.Services;

namespace KFlearning.Services
{
    public interface ITelemetryService : IUsesPersistence
    {
    }

    public class TelemetryService : ITelemetryService
    {
        private readonly ISystemInfoService _infoService;
        private readonly ITelemetryClient _telemetry;
        private readonly ILogger _logger;

        public TelemetryService(ILogger logger, ITelemetryClient telemetry, ISystemInfoService infoService)
        {
            _logger = logger;
            _telemetry = telemetry;
            _infoService = infoService;
        }

        public void Load()
        {
            try
            {
                _infoService.Query();
                LogDevice();

                _logger.Debug("Sending telemetry");
                Task.WaitAll(
                    _telemetry.SendTelemetry(new UserEngagementModel
                    {
                        DeviceId = _infoService.DeviceId,
                        AppName = "kflearning",
                        Event = "app_start"
                    }),
                    _telemetry.SendIdentification(new DeviceIdentificationModel
                    {
                        DeviceId = _infoService.DeviceId,
                        CPU = _infoService.CPU,
                        RAM = _infoService.RAM,
                        OS = _infoService.OS,
                        Architecture = _infoService.Architecture
                    })
                );
            }
            catch (Exception ex)
            {
                _logger.Error("Cannot post telemetry data", ex);
            }
        }

        public void Save()
        {
            try
            {
                _infoService.Query();

                _logger.Debug("Sending telemetry");
                Task.WaitAll(_telemetry.SendTelemetry(new UserEngagementModel
                {
                    DeviceId = _infoService.DeviceId,
                    AppName = "kflearning",
                    Event = "app_stop"
                }));
            }
            catch (Exception ex)
            {
                _logger.Error("Cannot post telemetry data", ex);
            }
        }

        private void LogDevice()
        {
            _logger.DebugFormat("CPU: {0}", _infoService.CPU);
            _logger.DebugFormat("RAM: {0}", _infoService.RAM);
            _logger.DebugFormat("OS: {0}", _infoService.OS);
            _logger.DebugFormat("OS version: {0}", _infoService.OSVersion);
            _logger.DebugFormat("Architecture: {0}", _infoService.Architecture);
            _logger.DebugFormat("Device ID: {0}", _infoService.DeviceId);
        }
    }
}