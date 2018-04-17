namespace GodSharp.Mina
{
    public class MinaServiceOption
    {
        public string ServiceName { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool AutoLog { get; set; } = true;
        public bool CanStop { get; set; } = true;
        public bool CanShutdown { get; set; } = false;
        public bool CanPauseAndContinue { get; set; } = false;
        public bool CanHandlePowerEvent { get; set; } = false;
        public bool CanHandleSessionChangeEvent { get; set; } = false;
    }
}
