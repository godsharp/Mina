using System;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;

namespace GodSharp.Mina
{
    /// <summary>
    /// <see cref="ServiceController"/> helper.
    /// </summary>
    public class ServiceControllerHelper
    {
        /// <summary>
        /// Exists the specified service name.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <returns></returns>
        public static bool Exist(string serviceName)
        {
            return ServiceController.GetServices().FirstOrDefault(f => f.ServiceName == serviceName) == null;
        }

        /// <summary>
        /// Starts the specified service name.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="throwException">if set to <c>true</c> [throw exception].</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        public static bool Start(string serviceName, bool throwException = true, int timeout = 50000)
        {
            return Action(serviceName, ServiceControllerStatus.Running, (sc) => sc.Start(), timeout, throwException);
        }

        /// <summary>
        /// Restarts the specified service name.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="throwException">if set to <c>true</c> [throw exception].</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        public static bool ReStart(string serviceName, bool throwException = true, int timeout = 50000)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            bool ret = Stop(serviceName, throwException, timeout);

            stopwatch.Stop();

            int span = timeout - (int)stopwatch.Elapsed.TotalMilliseconds;

            return ret ? Start(serviceName, throwException, span > 100 ? span : 100) : false;
        }

        /// <summary>
        /// Stops the specified service name.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="throwException">if set to <c>true</c> [throw exception].</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        public static bool Stop(string serviceName, bool throwException = true, int timeout = 50000)
        {
            return Action(serviceName, ServiceControllerStatus.Stopped, (sc) => sc.Stop(), timeout, throwException);
        }

        /// <summary>
        /// Pauses the specified service name.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="throwException">if set to <c>true</c> [throw exception].</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        public static bool Pause(string serviceName, bool throwException = true, int timeout = 50000)
        {
            return Action(serviceName, ServiceControllerStatus.Paused, (sc) => sc.Pause(), timeout, throwException);
        }

        /// <summary>
        /// Continues the specified service name.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="throwException">if set to <c>true</c> [throw exception].</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        public static bool Continue(string serviceName, bool throwException = true, int timeout = 50000)
        {
            return Action(serviceName, ServiceControllerStatus.Running, (sc) => sc.Continue(), timeout, throwException);
        }

        /// <summary>
        /// Commands the specified service name.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="command">The command.</param>
        /// <param name="throwException">if set to <c>true</c> [throw exception].</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        public static bool Command(string serviceName,int command, bool throwException = true, int timeout = 50000)
        {
            return Action(serviceName, ServiceControllerStatus.Running, (sc) => sc.ExecuteCommand(command), timeout, throwException);
        }

        /// <summary>
        /// Actions the specified service name.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="status">The status.</param>
        /// <param name="action">The action.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="throwException">if set to <c>true</c> [throw exception].</param>
        /// <returns></returns>
        private static bool Action(string serviceName, ServiceControllerStatus status,Action<ServiceController> action, int timeout = 50000, bool throwException = true)
        {
            try
            {
                using (ServiceController serviceController = new ServiceController(serviceName))
                {
                    if (serviceController.Status != status)
                    {
                        action?.Invoke(serviceController);

                        serviceController.WaitForStatus(status, new TimeSpan(0, 0, 0, 0, timeout));
                    }

                    return serviceController.Status == status;
                }
            }
            catch (Exception ex)
            {
                if (throwException) throw ex;
                return false;
            }
        }
    }
}
