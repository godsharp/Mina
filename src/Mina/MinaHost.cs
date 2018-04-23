using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;

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

                bool help = false;
                bool install = false;
                bool uninstall = false;
                bool start = false;
                bool stop = false;
                bool restart = false;
                bool pause = false;
                bool continuee = false;
                bool command = false;
                int cmd = -1;

                string parameter = null;

                if (args?.Length > 0)
                {
                    int length = args.Length;
                    string action = args[0];

                    help = action.Equals("-h") || action.Equals("/h") || action.Equals("-help") || action.Equals("/help");

                    install = action.Equals("-i") || action.Equals("/i") || action.Equals("-install") || action.Equals("/install");

                    if (install && length > 1)
                    {
                        for (int i = 1; i < length; i++)
                        {
                            if (!Extension.IsNullOrWhiteSpace(args[i])) parameter += $"{args[i]} ";
                        }

                        parameter = parameter.Trim();
                    }

                    uninstall = action.Equals("-u") || action.Equals("/u") || action.Equals("-uninstall") || action.Equals("/uninstall");

                    start = action.Equals("-start") || action.Equals("/start");
                    stop = action.Equals("-stop") || action.Equals("/stop");
                    restart = action.Equals("-r") || action.Equals("/r") || action.Equals("-restart") || action.Equals("/restart");
                    
                    pause = action.Equals("-p") || action.Equals("/p") || action.Equals("-pause") || action.Equals("/pause");
                    continuee = action.Equals("-c") || action.Equals("/c") || action.Equals("-continue") || action.Equals("/continue");

                    command = action.Equals("-cmd") || action.Equals("/cmd") || action.Equals("-commnd") || action.Equals("/commnd");

                    if (command && length > 1)
                    {
                        bool ret = int.TryParse(args[1], out cmd);
                        command = ret && cmd != -1;

                        if (!command) return;
                    }
                }
                
                if (help)
                {
                    PrintHelp();
                    return;
                }

                if (start)
                {
                    ExecuteActionCheckServiceExist(option.Service.ServiceName, () => ServiceControllerHelper.Start(option.Service.ServiceName));
                    return;
                }

                if (stop)
                {
                    ExecuteActionCheckServiceExist(option.Service.ServiceName, () => ServiceControllerHelper.Stop(option.Service.ServiceName));
                    return;
                }

                if (restart)
                {
                    ExecuteActionCheckServiceExist(option.Service.ServiceName, () => ServiceControllerHelper.ReStart(option.Service.ServiceName));
                    return;
                }

                if (pause)
                {
                    ExecuteActionCheckServiceExist(option.Service.ServiceName, () => ServiceControllerHelper.Pause(option.Service.ServiceName));
                    return;
                }

                if (continuee)
                {
                    ExecuteActionCheckServiceExist(option.Service.ServiceName, () => ServiceControllerHelper.Continue(option.Service.ServiceName));
                    return;
                }

                if (command)
                {
                    ServiceControllerHelper.Command(option.Service.ServiceName, cmd);
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

                    msi.Install(parameter);
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

                ExecuteActionCheckServiceExist(option.Service.ServiceName, null);

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

        private static void ExecuteActionCheckServiceExist(string serviceName, Action action)
        {
            if (!ServiceControllerHelper.Exist(serviceName)) throw new InvalidOperationException($"The service [{serviceName}] not install.");

            action?.Invoke();
        }

        private static void PrintHelp()
        {
            StringBuilder builder = new StringBuilder();
            Assembly assembly = Assembly.GetEntryAssembly();

            builder.AppendLine("Mina service help infomation");

            FileVersionInfo version = Extension.GetFileVersion(Assembly.GetExecutingAssembly().Location);
            builder.AppendLine($"Mina file version : {version.FileVersion}, product version : {version.ProductVersion}.");

            version = Extension.GetFileVersion();
            builder.AppendLine($"Service file version : {version.FileVersion}, product version : {version.ProductVersion}.");
            builder.AppendLine("Syntax");
            builder.AppendLine("\tSampleService.exe option");
            builder.AppendLine("Options");
            builder.AppendLine("\t-h[elp] print help information");
            builder.AppendLine("\t-i[nstall] [startup-parameter] install service with startup parameter when startup-parameter is not null");
            builder.AppendLine("\t-u[install] uinstall service");
            builder.AppendLine("\t-start start the service");
            builder.AppendLine("\t-r[estart] restart the service");
            builder.AppendLine("\t-p[ause] pause the service");
            builder.AppendLine("\t-c[ontinue] continue the service");
            builder.AppendLine("\t-stop stop the service");
            builder.AppendLine("\t-command[cmd] custom-command-parameter execute custom command with custom-command-parameter,custom-command-parameter must be 1 to 128");

            Console.WriteLine(builder.ToString());
        }
    }
}
