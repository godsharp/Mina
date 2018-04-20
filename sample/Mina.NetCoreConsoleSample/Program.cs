using GodSharp.Mina;
using System;

namespace Mina.NetCoreConsoleSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Mina!");
            //char ch;
            //ConsoleKeyInfo info;

            //do
            //{
            //    Help();
            //    info = Console.ReadKey();
            //    ch = info.KeyChar;
            //} while (!(ch == 96 || ch == 97 || ch == 48 || ch == 49));

            //if (args == null || args.Length == 0) args = new string[1];

            //args[0] = ch == 96 || ch == 48 ? "-u" : "-i";

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
            }, new MinaSampleService(), args, null);

            Console.WriteLine("Press Ctrl+C to exit!");

            do { } while (Console.ReadKey(true).KeyChar != (char)ConsoleSpecialKey.ControlC);
        }

        private static void Help()
        {
            Console.WriteLine();
            Console.WriteLine("Select Option:");
            Console.WriteLine("\t1.install");
            Console.WriteLine("\t0.uninstall");
            Console.Write("input option number[1/0]:");
        }
    }
}
