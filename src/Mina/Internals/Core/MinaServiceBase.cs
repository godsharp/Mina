using System;
using System.ServiceProcess;

namespace GodSharp.Mina
{
    internal class MinaServiceBase: ServiceBase
    {
        MinaService service;
        MinaServiceOption options;

        public MinaServiceBase() { }

        public MinaServiceBase(MinaService service, MinaServiceOption option)
        {
            this.service = service;
            this.options = option;

            SetOptions();
        }

        public void SetService(MinaService service)
        {
            this.service = service;
        }

        public void SetOption(Action<MinaServiceOption> action)
        {
            action(options);

            SetOptions();
        }

        private void SetOptions()
        {
            this.ServiceName = options.ServiceName;
            this.AutoLog = options.AutoLog;
            this.CanStop = options.CanStop;
            this.CanPauseAndContinue = options.CanPauseAndContinue;
            this.CanShutdown = options.CanShutdown;
            this.CanHandleSessionChangeEvent = options.CanHandleSessionChangeEvent;
            this.CanHandlePowerEvent = options.CanHandlePowerEvent;
        }

        protected override void OnContinue() => service.Continue();
        protected override void OnCustomCommand(int command) => service.CustomCommand(command);
        protected override void OnPause() => service.Pause();
        protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus) => service.PowerEvent(powerStatus);
        protected override void OnSessionChange(SessionChangeDescription changeDescription) => service.SessionChange(changeDescription);
        protected override void OnShutdown() => service.Shutdown();
        protected override void OnStart(string[] args) => service.Start(args);
        protected override void OnStop() => service.Stop();
    }
}
