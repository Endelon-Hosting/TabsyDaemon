using Docker.DotNet;

using System;
using System.Threading.Tasks;

using TabsyDaemon.Controller;
using TabsyDaemon.Interfaces;
using TabsyDaemon.Logging;

namespace TabsyDaemon.Services
{
    public class DockerService : ITabsyService
    {
        public static DockerClient DockerClient { get; set; }

        public bool IsRunning
        {
            get
            {
                if (DockerClient == null)
                    return false;

                return true;
            }
        }

        public async void Start()
        {
            DockerClient = new DockerClientConfiguration(
                new Uri(Environment.DockerConnectionUrl))
                    .CreateClient();

            try
            {
                DockerController.GetContainers().Wait();
                Logger.Info("Docker connection sucessfully");
            }
            catch(Exception e)
            {
                Logger.Warn($"Cannot connect to local docker engine: {e.Message}");
            }
        }

        public async void Stop()
        {
            DockerClient.Dispose();
            DockerClient = null;
        }
    }
}
