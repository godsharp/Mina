using GodSharp.Mina;
using System;

namespace Mina.NetCoreConsoleSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("Press Ctrl + C exit!");

            MinaHost.Run(new MinaOption()
            {
                Service = new MinaServiceOption()
                {
                    ServiceName = "MinaSampleService",
                    DisplayName = "A Mina Sample Service",
                    Description = "Description for Mina Sample Service",
                    AutoLog = true,
                    CanStop = true
                },
                Startup = new MinaStatrupOption()
                {
                    StartType = System.ServiceProcess.ServiceStartMode.Automatic,
                    DelayedAutoStart = false
                },
                Account = new MinaAccountOption()
                { ServiceAccount = System.ServiceProcess.ServiceAccount.LocalSystem }
            }, new MinaSampleService(), args);

            do { } while (Console.ReadKey(true).KeyChar != (char)ConsoleSpecialKey.ControlC);
        }
    }
}
