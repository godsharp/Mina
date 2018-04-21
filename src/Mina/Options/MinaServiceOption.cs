namespace GodSharp.Mina
{
    /// <summary>
    /// The base option of service.
    /// </summary>
    public class MinaServiceOption
    {
        /// <summary>
        /// Gets or sets the name of the service.
        /// </summary>
        /// <value>
        /// The name of the service.
        /// </value>
        public string ServiceName { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [automatic log].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [automatic log]; otherwise, <c>false</c>.
        /// </value>
        public bool AutoLog { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether this instance can stop.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can stop; otherwise, <c>false</c>.
        /// </value>
        public bool CanStop { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether this instance can shutdown.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can shutdown; otherwise, <c>false</c>.
        /// </value>
        public bool CanShutdown { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether this instance can pause and continue.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can pause and continue; otherwise, <c>false</c>.
        /// </value>
        public bool CanPauseAndContinue { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether this instance can handle power event.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can handle power event; otherwise, <c>false</c>.
        /// </value>
        public bool CanHandlePowerEvent { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether this instance can handle session change event.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can handle session change event; otherwise, <c>false</c>.
        /// </value>
        public bool CanHandleSessionChangeEvent { get; set; } = false;

        /// <summary>
        /// Gets or sets the exit code for the service.
        /// </summary>
        /// <value>
        /// The exit code.
        /// </value>
        public int ExitCode { get; set; } = 0;


    }
}
