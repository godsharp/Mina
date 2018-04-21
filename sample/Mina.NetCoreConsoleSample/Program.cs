using GodSharp.Mina;
using System;

namespace Mina.NetCoreConsoleSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Mina!");

            MinaHost.Run<MinaSampleService>((o) =>
            {
                o.RunServiceAfterInstall = true;
                o.Service.ServiceName = "MinaSampleService";
                o.Service.DisplayName = "A Mina Sample Service";
                o.Service.Description = "Description for Mina Sample Service";
            }, args);
            
            do { } while (Console.ReadKey(true).KeyChar != (char)ConsoleSpecialKey.ControlC);
        }
    }
}
