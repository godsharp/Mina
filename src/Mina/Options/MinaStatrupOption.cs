using System.ServiceProcess;

namespace GodSharp.Mina
{
    /// <summary>
    /// The startup option of service.
    /// </summary>
    public class MinaStatrupOption
    {
        /// <summary>
        /// Gets or sets the start type.
        /// </summary>
        /// <value>
        /// The start type.
        /// </value>
        public ServiceStartMode StartType { get; set; } = ServiceStartMode.Automatic;

        /// <summary>
        /// Gets or sets a value indicating whether [delayed automatic start].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [delayed automatic start]; otherwise, <c>false</c>.
        /// </value>
        public bool DelayedAutoStart { get; set; } = false;

        /// <summary>
        /// Gets or sets the services depended on.
        /// </summary>
        /// <value>
        /// The services depended on.
        /// </value>
        public string[] ServicesDependedOn { get; set; }
    }
}
