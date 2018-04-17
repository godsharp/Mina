using GodSharp.Mina;
namespace Mina.NetCoreConsoleSample
{
    public class MinaSampleService:MinaService
    {
        public override void Start(string[] args)
        {
            base.Start(args);
        }

        public override void Stop()
        {
            base.Stop();
        }

        public override void Pause()
        {
            base.Pause();
        }

        public override void Continue()
        {
            base.Continue();
        }

        public override void Shutdown()
        {
            base.Shutdown();
        }

        public override void CustomCommand(int command)
        {
            base.CustomCommand(command);
        }
    }
}
