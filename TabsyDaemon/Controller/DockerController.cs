using Docker.DotNet;
using Docker.DotNet.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

using TabsyDaemon.Docker;
using TabsyDaemon.Services;

namespace TabsyDaemon.Controller
{
    public class DockerController
    {
        private static DockerClient DockerClient { get { return DockerService.DockerClient; } }

        public async Task<List<DockerContainer>> GetContainers()
        {
            List<DockerContainer> result = new List<DockerContainer>();

            IList<ContainerListResponse> containers = await DockerClient.Containers.ListContainersAsync(
            new ContainersListParameters());

            foreach(ContainerListResponse clr in containers)
            {
                DockerContainer c = new DockerContainer()
                {
                    Command = clr.Command,
                    Created = clr.Created,
                    ID = clr.ID,
                    Image = clr.Image,
                    ImageID = clr.ImageID,
                    Labels = clr.Labels,
                    Mounts = clr.Mounts,
                    Names = clr.Names,
                    NetworkSettings = clr.NetworkSettings,
                    Ports = clr.Ports,
                    SizeRootFs = clr.SizeRootFs,
                    SizeRw = clr.SizeRw,
                    State = clr.State,
                    Status = clr.Status
                };

                result.Add(c);
            }

            return result;
        }
    }
}
