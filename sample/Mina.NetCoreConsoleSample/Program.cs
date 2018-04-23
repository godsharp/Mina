using GodSharp.Mina;

namespace Mina.NetCoreConsoleSample
{
    class Program
    {
        static void Main(string[] args)
        {
            MinaHost.Run<MinaSampleService>((o) =>
            {
                o.RunServiceAfterInstall = true;
                o.Service.ServiceName = "MinaSampleService";
                o.Service.DisplayName = "A Mina Sample Service";
                o.Service.Description = "Description for Mina Sample Service";
            }, args);
        }
    }
}
