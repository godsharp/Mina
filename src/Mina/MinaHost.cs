using System;
using System.Linq;
using System.ServiceProcess;

namespace GodSharp.Mina
{
    /// <summary>
    /// The host to run service.
    /// </summary>
    public class MinaHost
    {
        /// <summary>
        /// Runs the specified option.
        /// </summary>
        /// <param name="option">The option.</param>
        /// <param name="service">The service.</param>
        /// <param name="args">The arguments.</param>
        /// <param name="installer">The installer.</param>
        /// <exception cref="ArgumentNullException">
        /// option
        /// or
        /// service
        /// </exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static void Run(MinaOption option, MinaService service, string[] args = null, MinaInstaller installer = null)
        {
            try
            {
                // parameter check
                if (option == null) throw new ArgumentNullException(nameof(option));
                if (service == null) throw new ArgumentNullException(nameof(service));

                if (string.IsNullOrWhiteSpace(option.Service.ServiceName)) throw new ArgumentNullException(nameof(option.Service.ServiceName));
                if (string.IsNullOrWhiteSpace(option.Service.DisplayName)) option.Service.DisplayName = option.Service.ServiceName;

                // service initialize
                service.OnInitialize();

                bool install = false;
                bool uninstall = false;

                if (args?.Length > 0)
                {
                    install = args.Contains("-i") || args.Contains("/i") || args.Contains("-install") || args.Contains("/install");
                    uninstall = args.Contains("-u") || args.Contains("/u") || args.Contains("-uninstall") || args.Contains("/uninstall");
                }

                // initialize installer
                MinaServiceInstaller msi = null;

                if (install || uninstall)
                {
                    msi = new MinaServiceInstaller(option);
                }

                // install service
                if (install)
                {
                    installer?.OnBeforeInstall();

                    msi.Install();
                    msi.Dispose();

                    installer?.OnAfterInstall();

                    if (option.RunServiceAfterInstall) ServiceControllerHelper.Start(option.Service.ServiceName);

                    return;
                }

                // uninstall service
                if (uninstall)
                {
                    installer?.OnBeforeUninstall();

                    msi.Uninstall();
                    msi.Dispose();

                    installer?.OnAfterUninstall();
                    return;
                }

                if (!ServiceControllerHelper.Exist(option.Service.ServiceName)) throw new InvalidOperationException($"The service [{option.Service.ServiceName}] not install.");

                // run service
                ServiceBase[] ServicesToRun;

                ServicesToRun = new ServiceBase[]
                {
                new MinaServiceBase(service,option.Service)
                };

                ServiceBase.Run(ServicesToRun);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
