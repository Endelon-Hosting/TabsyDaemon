using Docker.DotNet;

using System;
using System.Threading.Tasks;

using TabsyDaemon.Interfaces;

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

        public async Task Start()
        {
            DockerClient = new DockerClientConfiguration(
                new Uri("unix:///var/run/docker.sock"))
                    .CreateClient();
        }

        public async Task Stop()
        {
            DockerClient.Dispose();
            DockerClient = null;
        }
    }
}
