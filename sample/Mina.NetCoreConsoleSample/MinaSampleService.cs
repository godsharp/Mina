using GodSharp.Mina;
using System;
using System.IO;
using System.Threading;

namespace Mina.NetCoreConsoleSample
{
    public class MinaSampleService:MinaService
    {
        bool run = true;
        int index = 0;
        string file;

        public override void OnStart(string[] args)
        {
            if (args?.Length > 0 == true)
            {
                foreach (var item in args)
                {
                    File.AppendAllText(file, string.Join(",", args));
                }
            }

            index = 0;
            file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tmp.log");

            ThreadPool.QueueUserWorkItem((obj) =>
            {
                while (run)
                {
                    File.AppendAllText(file, $"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}] Hello number:{index++}\r\n");
                    Thread.Sleep(5000);
                }
            });
        }

        public override void OnStop()
        {
            run = false;
        }

        public override void OnPause()
        {
            //base.Pause();
        }

        public override void OnContinue()
        {
            //base.Continue();
        }

        public override void OnShutdown()
        {
            //base.Shutdown();
        }

        public override void OnCustomCommand(int command)
        {
            //base.CustomCommand(command);
        }
    }
}
