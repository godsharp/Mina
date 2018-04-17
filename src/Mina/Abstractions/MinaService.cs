using System;
using System.ServiceProcess;

namespace GodSharp.Mina
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class MinaService
    {
        public virtual void Continue() { throw new NotImplementedException(); }
        public virtual void CustomCommand(int command) { throw new NotImplementedException(); }
        public virtual void Pause() { throw new NotImplementedException(); }
        public virtual bool PowerEvent(PowerBroadcastStatus powerStatus) { throw new NotImplementedException(); }
        public virtual void SessionChange(SessionChangeDescription changeDescription) { throw new NotImplementedException(); }
        public virtual void Shutdown() { throw new NotImplementedException(); }
        public virtual void Start(string[] args) { throw new NotImplementedException(); }
        public virtual void Stop() { throw new NotImplementedException(); }
    }
}
