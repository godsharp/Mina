using System;
using System.ServiceProcess;

namespace GodSharp.Mina
{
    /// <summary>
    /// The service wapper.
    /// </summary>
    /// <seealso cref="System.ServiceProcess.ServiceBase" />
    internal class MinaServiceBase: ServiceBase
    {
        /// <summary>
        /// The service
        /// </summary>
        private MinaService service;

        /// <summary>
        /// The options
        /// </summary>
        private MinaServiceOption options;

        /// <summary>
        /// Initializes a new instance of the <see cref="MinaServiceBase"/> class.
        /// </summary>
        public MinaServiceBase() { this.options = new MinaServiceOption(); }

        /// <summary>
        /// Initializes a new instance of the <see cref="MinaServiceBase"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="option">The option.</param>
        public MinaServiceBase(MinaService service, MinaServiceOption option) : this()
        {
            this.service = service;
            this.options = option;

            SetOptions();
        }

        /// <summary>
        /// Sets the service.
        /// </summary>
        /// <param name="service">The service.</param>
        public void SetService(MinaService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Sets the option.
        /// </summary>
        /// <param name="action">The action.</param>
        public void SetOption(Action<MinaServiceOption> action)
        {
            action(options);

            SetOptions();
        }

        /// <summary>
        /// Sets the options.
        /// </summary>
        private void SetOptions()
        {
            if (string.IsNullOrWhiteSpace(options.ServiceName))
            {
                throw new ArgumentNullException(nameof(options.ServiceName));
            }

            this.ServiceName = options.ServiceName;
            this.AutoLog = options.AutoLog;
            this.CanStop = options.CanStop;
            this.CanPauseAndContinue = options.CanPauseAndContinue;
            this.CanShutdown = options.CanShutdown;
            this.CanHandleSessionChangeEvent = options.CanHandleSessionChangeEvent;
            this.CanHandlePowerEvent = options.CanHandlePowerEvent;
        }

        /// <summary>
        /// When implemented in a derived class, <see cref="M:System.ServiceProcess.ServiceBase.OnContinue" /> runs when a Continue command is sent to the service by the Service Control Manager (SCM). Specifies actions to take when a service resumes normal functioning after being paused.
        /// </summary>
        protected override void OnContinue() => ChackInitialized(service.OnContinue);

        /// <summary>
        /// When implemented in a derived class, <see cref="M:System.ServiceProcess.ServiceBase.OnCustomCommand(System.Int32)" /> executes when the Service Control Manager (SCM) passes a custom command to the service. Specifies actions to take when a command with the specified parameter value occurs.
        /// </summary>
        /// <param name="command">The command message sent to the service.</param>
        protected override void OnCustomCommand(int command) => ChackInitialized(service.OnCustomCommand, command);

        /// <summary>
        /// When implemented in a derived class, executes when a Pause command is sent to the service by the Service Control Manager (SCM). Specifies actions to take when a service pauses.
        /// </summary>
        protected override void OnPause() => ChackInitialized(service.OnPause);

        /// <summary>
        /// When implemented in a derived class, executes when the computer's power status has changed. This applies to laptop computers when they go into suspended mode, which is not the same as a system shutdown.
        /// </summary>
        /// <param name="powerStatus">A <see cref="T:System.ServiceProcess.PowerBroadcastStatus" /> that indicates a notification from the system about its power status.</param>
        /// <returns>
        /// When implemented in a derived class, the needs of your application determine what value to return. For example, if a QuerySuspend broadcast status is passed, you could cause your application to reject the query by returning false.
        /// </returns>
        protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus) => ChackInitialized(service.OnPowerEvent, powerStatus);

        /// <summary>
        /// Executes when a change event is received from a Terminal Server session.
        /// </summary>
        /// <param name="changeDescription">A structure that identifies the change type.</param>
        protected override void OnSessionChange(SessionChangeDescription changeDescription) => ChackInitialized(service.OnSessionChange, changeDescription);

        /// <summary>
        /// When implemented in a derived class, executes when the system is shutting down. Specifies what should occur immediately prior to the system shutting down.
        /// </summary>
        protected override void OnShutdown() => ChackInitialized(service.OnShutdown);

        /// <summary>
        /// When implemented in a derived class, executes when a Start command is sent to the service by the Service Control Manager (SCM) or when the operating system starts (for a service that starts automatically). Specifies actions to take when the service starts.
        /// </summary>
        /// <param name="args">Data passed by the start command.</param>
        protected override void OnStart(string[] args) => ChackInitialized(service.OnStart, args);

        /// <summary>
        /// When implemented in a derived class, executes when a Stop command is sent to the service by the Service Control Manager (SCM). Specifies actions to take when a service stops running.
        /// </summary>
        protected override void OnStop() => ChackInitialized(service.OnStop);

        /// <summary>
        /// Chacks the initialized.
        /// </summary>
        /// <param name="action">The action.</param>
        private void ChackInitialized(Action action)
        {
            ChackInitialized();
            action.Invoke();
        }

        /// <summary>
        /// Chacks the initialized.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action">The action.</param>
        /// <param name="data">The data.</param>
        private void ChackInitialized<T>(Action<T> action,T data)
        {
            ChackInitialized();
            action.Invoke(data);
        }

        /// <summary>
        /// Chacks the initialized.
        /// </summary>
        /// <typeparam name="TInput">The type of the input.</typeparam>
        /// <typeparam name="TReturn">The type of the return.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        private TReturn ChackInitialized<TInput, TReturn>(Func<TInput,TReturn> action,TInput data)
        {
            ChackInitialized();
            return action.Invoke(data);
        }

        /// <summary>
        /// Chacks the initialized.
        /// </summary>
        /// <exception cref="NullReferenceException">
        /// </exception>
        private void ChackInitialized()
        {
            if (service == null) throw new NullReferenceException("The service is null, please invoke 'SetService' method.");
            if (options == null) throw new NullReferenceException("The option is null, please invoke 'SetOption' method.");
        }
    }
}
