using System.ServiceProcess;

namespace GodSharp.Mina
{
    /// <summary>
    /// The account option of service.
    /// </summary>
    public class MinaAccountOption
    {
        /// <summary>
        /// Gets or sets the service account.
        /// </summary>
        /// <value>
        /// The service account.
        /// </value>
        public ServiceAccount ServiceAccount { get; set; } = ServiceAccount.LocalSystem;

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; set; }
    }
}
