using System.Configuration.Install;
using System.ServiceProcess;

namespace GodSharp.Mina
{
    public class MinaHost
    {
        public static void Run(MinaOption option, MinaService service,params string[] args)
        {
            //switch (args[0].ToLower())
            //{
            //    case "-i":

            //        break;
            //    case "-u":
            //        break;
            //    default:
            //        Console.WriteLine("------参数说明------");
            //        Console.WriteLine("- i 安装服务！");
            //        Console.WriteLine("- u 卸载服务！");
            //        Console.ReadKey();
            //        return;
            //}

            bool install = false;
            bool uninstall = true;
            MinaServiceInstaller installer=null;

            if (install || uninstall)
            {
                installer = new MinaServiceInstaller();
                installer.Build(option);
            }

            if (install)
            {
                installer.Install();
                return;

            }

            if (uninstall)
            {
                installer.Uninstall();
                return;

            }


            ServiceBase[] ServicesToRun;

            ServicesToRun = new ServiceBase[]
            {
                new MinaServiceBase(service,option.Service)
            };

            ServiceBase.Run(ServicesToRun);
        }
    }
}
