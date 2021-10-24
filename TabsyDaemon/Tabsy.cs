using System;
using System.Collections.Generic;
using System.IO;

using TabsyDaemon.Interfaces;
using TabsyDaemon.Logging;
using TabsyDaemon.Services;

namespace TabsyDaemon
{
    public class Tabsy
    {
        public List<ITabsyService> Services { get; set; }

        public Tabsy()
        {
            Services = new List<ITabsyService>()
            {
                new DockerService(),
                new RpcService()
            };
        }

        public void Start()
        {
            Logger.Info($"Starting TabsyDaemon v{Environment.Version}");
            Logger.Info("© 2021 Endelon Hosting");
            Logger.Info("www.endelon-hosting.de");

            #region Services
            Logger.Info("Starting services");

            int startedServices = 0;

            foreach(ITabsyService service in Services)
            {
                try
                {
                    service.Start();
                    Logger.Info($"Started service {service.GetType().Name}");

                    startedServices++;
                }
                catch(Exception e)
                {
                    Logger.Error($"Cannot start service {service.GetType().Name}. Is your daemon on the newest version? Error message: {e.Message}");
                }
            }

            Logger.Info($"Started {startedServices} services");

            #endregion

            #region Filesystem

            Directory.CreateDirectory(Environment.TabsyDirectory);
            Directory.CreateDirectory(Environment.TabsyContainerDirectory);
            Directory.CreateDirectory(Environment.TabsyImagesDirectory);

            #endregion
        }
    }
}
