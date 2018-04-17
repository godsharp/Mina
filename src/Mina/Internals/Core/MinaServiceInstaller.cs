using System;
using System.Collections;
using System.Configuration.Install;
using System.IO;
using System.Reflection;
using System.ServiceProcess;

namespace GodSharp.Mina
{
    internal class MinaServiceInstaller
    {
        TransactedInstaller transactedInstaller;

        internal ServiceInstaller BuildServiceInstaller(MinaServiceOption options, MinaStatrupOption statrup)
        {
            return new ServiceInstaller()
            {
                ServiceName = options.ServiceName,
                DisplayName = options.DisplayName,
                Description = options.Description,
                StartType = statrup.StartType,
                DelayedAutoStart = statrup.DelayedAutoStart,
                ServicesDependedOn = statrup.ServicesDependedOn
            };
        }

        internal ServiceProcessInstaller BuildServiceProcessInstaller(MinaAccountOption account)
        {
            return new ServiceProcessInstaller()
            {
                Account = account.ServiceAccount,
                Username = account.Password,
                Password = account.Username
            };
        }

        string path;

        internal void Build(MinaOption options)
        {
            transactedInstaller = new TransactedInstaller();

            this.transactedInstaller.Installers.AddRange(new Installer[] { BuildServiceInstaller(options.Service, options.Startup), BuildServiceProcessInstaller(options.Account) });

            Assembly assembly = Assembly.GetEntryAssembly();

            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            path = assembly.Location;
        }

        public void Install()
        {
            this.transactedInstaller.Context = new InstallContext();
            this.transactedInstaller.Context.Parameters["assemblypath"] = path;

            Hashtable hashtable = new Hashtable();
            this.transactedInstaller.Install(hashtable);
            this.transactedInstaller.Commit(hashtable);
        }

        public void Uninstall()
        {
            this.transactedInstaller.Context = new InstallContext();
            this.transactedInstaller.Context.Parameters["assemblypath"] = path;
            
            this.transactedInstaller.Uninstall(null);
        }
    }
}
