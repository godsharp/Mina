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
        /// Runs the service with specified option.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option">The option.</param>
        /// <param name="args">The arguments.</param>
        /// <param name="installer">The installer.</param>
        public static void Run<T>(MinaOption option, string[] args = null, MinaInstaller installer = null) where T : MinaService, new()
        {
            try
            {
                Run(option, new T(), args, installer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Runs the service with specified option.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option">The option.</param>
        /// <param name="args">The arguments.</param>
        /// <param name="installer">The installer.</param>
        /// <exception cref="ArgumentNullException">option</exception>
        public static void Run<T>(Action<MinaOption> option, string[] args = null, MinaInstaller installer = null) where T : MinaService, new()
        {
            try
            {
                if (option == null) throw new ArgumentNullException(nameof(option));

                MinaOption _option = new MinaOption();

                option.Invoke(_option);

                Run(_option, new T(), args, installer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Runs the service with specified option.
        /// </summary>
        /// <param name="option">The option.</param>
        /// <param name="service">The service.</param>
        /// <param name="args">The arguments.</param>
        /// <param name="installer">The installer.</param>
        /// <exception cref="ArgumentNullException">option</exception>
        public static void Run(Action<MinaOption> option, MinaService service, string[] args = null, MinaInstaller installer = null)
        {
            try
            {
                if (option == null) throw new ArgumentNullException(nameof(option));

                MinaOption _option = new MinaOption();

                option.Invoke(_option);

                Run(_option, service, args, installer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Runs the service with specified option.
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
                
                if (Extension.IsNullOrWhiteSpace(option.Service.ServiceName))
                {
                    throw new ArgumentNullException(nameof(option.Service.ServiceName));
                }
                
                if (Extension.IsNullOrWhiteSpace(option.Service.ServiceName))
                {
                    option.Service.DisplayName = option.Service.ServiceName;
                }
                
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
