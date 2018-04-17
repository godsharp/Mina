using System.ServiceProcess;

namespace GodSharp.Mina
{
    public class MinaAccountOption
    {
        public ServiceAccount ServiceAccount { get; set; } = ServiceAccount.LocalSystem;
        public string Password { get; set; }
        public string Username { get; set; }
    }
}
