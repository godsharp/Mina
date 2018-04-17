using System.ServiceProcess;

namespace GodSharp.Mina
{
    public class MinaStatrupOption
    {
        public ServiceStartMode StartType { get; set; }
        public bool DelayedAutoStart { get; set; }
        public string[] ServicesDependedOn { get; set; }
    }
}
