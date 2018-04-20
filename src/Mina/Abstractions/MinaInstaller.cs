namespace GodSharp.Mina
{
    /// <summary>
    /// The base class for service installer.
    /// </summary>
    public abstract class MinaInstaller
    {
        /// <summary>
        /// Called when [before install].
        /// </summary>
        public virtual void OnBeforeInstall() { }

        /// <summary>
        /// Called when [after install].
        /// </summary>
        public virtual void OnAfterInstall() { }

        /// <summary>
        /// Called when [before uninstall].
        /// </summary>
        public virtual void OnBeforeUninstall() { }

        /// <summary>
        /// Called when [after uninstall].
        /// </summary>
        public virtual void OnAfterUninstall() { }
    }
}
