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
                bool start = false;
                bool stop = false;
                bool restart = false;
                bool pause = false;
                bool continuee = false;
                bool command = false;
                int cmd = -1;

                if (args?.Length > 0)
                {
                    install = args.Contains("-i") || args.Contains("/i") || args.Contains("-install") || args.Contains("/install");
                    uninstall = args.Contains("-u") || args.Contains("/u") || args.Contains("-uninstall") || args.Contains("/uninstall");

                    start = args.Contains("-start") || args.Contains("/start");
                    stop = args.Contains("-stop") || args.Contains("/stop");
                    restart = args.Contains("-r") || args.Contains("/r") || args.Contains("-restart") || args.Contains("/restart");
                    
                    pause = args.Contains("-p") || args.Contains("/p") || args.Contains("-pause") || args.Contains("/pause");
                    continuee = args.Contains("-c") || args.Contains("/c") || args.Contains("-continue") || args.Contains("/continue");

                    command = args.Contains("-cmd") || args.Contains("/cmd") || args.Contains("-commnd") || args.Contains("/commnd");

                    if (command)
                    {
                        string[] cmds = new string[] { "-cmd", "/cmd", "-commnd", "/commnd" };
                        foreach (var item in cmds)
                        {
                            int index = Array.IndexOf(args, item);
                            index++;

                            if (index > 0 && args.Length >= index)
                            {
                                bool ret = int.TryParse(args[index], out cmd);
                                break;
                            }
                        }
                    }
                }

                if (start)
                {
                    ServiceControllerHelper.Start(option.Service.ServiceName);
                    return;
                }

                if (stop)
                {
                    ServiceControllerHelper.Stop(option.Service.ServiceName);
                    return;
                }

                if (restart)
                {
                    ServiceControllerHelper.ReStart(option.Service.ServiceName);
                    return;
                }

                if (pause)
                {
                    ServiceControllerHelper.Pause(option.Service.ServiceName);
                    return;
                }
                if (continuee)
                {
                    ServiceControllerHelper.Continue(option.Service.ServiceName);
                    return;
                }

                if (command)
                {
                    if (cmd != -1) ServiceControllerHelper.Command(option.Service.ServiceName, cmd);
                    return;
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
