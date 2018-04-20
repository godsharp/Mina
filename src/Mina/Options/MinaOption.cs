namespace GodSharp.Mina
{
    /// <summary>
    /// Mina options.
    /// </summary>
    public class MinaOption
    {
        /// <summary>
        /// Gets or sets a value indicating whether [run service after install].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [run service after install]; otherwise, <c>false</c>.
        /// </value>
        public bool RunServiceAfterInstall { get; set; } = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="MinaOption"/> class.
        /// </summary>
        public MinaOption()
        {
            Service = new MinaServiceOption();
            Account = new MinaAccountOption();
            Startup = new MinaStatrupOption();
        }

        /// <summary>
        /// Gets or sets the service base options of type <see cref="MinaServiceOption"/>.
        /// </summary>
        /// <value>
        /// The service.
        /// </value>
        public MinaServiceOption Service { get; set; }

        /// <summary>
        /// Gets or sets the service account options of type <see cref="MinaAccountOption"/>.
        /// </summary>
        /// <value>
        /// The account.
        /// </value>
        public MinaAccountOption Account { get; set; }

        /// <summary>
        /// Gets or sets the service startup options of type <see cref="MinaStatrupOption"/>.
        /// </summary>
        public MinaStatrupOption Startup { get; set; }
    }
}
