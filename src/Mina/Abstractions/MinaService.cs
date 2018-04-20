using System.ServiceProcess;

namespace GodSharp.Mina
{
    /// <summary>
    /// The base class for service.
    /// </summary>
    public abstract class MinaService
    {
        /// <summary>
        /// Called when [continue].
        /// </summary>
        public virtual void OnContinue() { }

        /// <summary>
        /// Called when [custom command].
        /// </summary>
        /// <param name="command">The command.</param>
        public virtual void OnCustomCommand(int command) { }

        /// <summary>
        /// Called when [pause].
        /// </summary>
        public virtual void OnPause() { }

        /// <summary>
        /// Called when [power event].
        /// </summary>
        /// <param name="powerStatus">The power status.</param>
        /// <returns></returns>
        public virtual bool OnPowerEvent(PowerBroadcastStatus powerStatus) { return true; }

        /// <summary>
        /// Called when [session change].
        /// </summary>
        /// <param name="changeDescription">The change description.</param>
        public virtual void OnSessionChange(SessionChangeDescription changeDescription) { }

        /// <summary>
        /// Called when [shutdown].
        /// </summary>
        public virtual void OnShutdown() { }

        /// <summary>
        /// Called when [start].
        /// </summary>
        /// <param name="args">The arguments.</param>
        public virtual void OnStart(string[] args) { }

        /// <summary>
        /// Called when [stop].
        /// </summary>
        public virtual void OnStop() { }

        /// <summary>
        /// Called when [initialize].
        /// </summary>
        public virtual void OnInitialize() { }
    }
}
