﻿using System;
using System.Collections;
using System.Configuration.Install;
using System.IO;
using System.Reflection;
using System.ServiceProcess;

namespace GodSharp.Mina
{
    /// <summary>
    /// The installer for service.
    /// </summary>
    internal class MinaServiceInstaller : IDisposable
    {
        /// <summary>
        /// The transacted installer
        /// </summary>
        private TransactedInstaller transactedInstaller;
        private string path;

        /// <summary>
        /// Initializes a new instance of the <see cref="MinaServiceInstaller"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public MinaServiceInstaller(MinaOption options)
        {
            Build(options);
        }

        /// <summary>
        /// Builds the service installer.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="statrup">The statrup.</param>
        /// <returns></returns>
        internal ServiceInstaller BuildServiceInstaller(MinaServiceOption options, MinaStatrupOption statrup)
        {
            return new ServiceInstaller()
            {
                ServiceName = options.ServiceName,
                DisplayName = options.DisplayName,
                Description = options.Description,
                StartType = statrup.StartType,
#if !(NET20||NET35)
                DelayedAutoStart = statrup.DelayedAutoStart, 
#endif
                ServicesDependedOn = statrup.ServicesDependedOn
            };
        }

        /// <summary>
        /// Builds the service process installer.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <returns></returns>
        internal ServiceProcessInstaller BuildServiceProcessInstaller(MinaAccountOption account)
        {
            return new ServiceProcessInstaller()
            {
                Account = account.ServiceAccount,
                Username = account.Password,
                Password = account.Username
            };
        }

        /// <summary>
        /// Builds the specified options.
        /// </summary>
        /// <param name="options">The options.</param>
        internal void Build(MinaOption options)
        {
            transactedInstaller = new TransactedInstaller();

            this.transactedInstaller.Installers.AddRange(new Installer[] { BuildServiceInstaller(options.Service, options.Startup), BuildServiceProcessInstaller(options.Account) });

            Assembly assembly = Assembly.GetEntryAssembly();

            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            path = assembly.Location;
        }
        
        /// <summary>
        /// Installs this instance.
        /// </summary>
        public void Install()
        {
            this.transactedInstaller.Context = new InstallContext("Install.log", null);
            this.transactedInstaller.Context.Parameters["assemblypath"] = path;

            Hashtable hashtable = new Hashtable();
            this.transactedInstaller.Install(hashtable);
        }

        /// <summary>
        /// Uninstalls this instance.
        /// </summary>
        public void Uninstall()
        {
            this.transactedInstaller.Context = new InstallContext("Install.log", null);
            this.transactedInstaller.Context.Parameters["assemblypath"] = path;
            
            this.transactedInstaller.Uninstall(null);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    this.transactedInstaller.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~MinaServiceInstaller()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
