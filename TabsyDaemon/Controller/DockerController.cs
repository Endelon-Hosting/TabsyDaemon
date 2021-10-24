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

        public static async Task<List<DockerContainer>> GetContainers()
        {
            List<DockerContainer> result = new List<DockerContainer>();

            IList<ContainerListResponse> containers = await DockerClient.Containers.ListContainersAsync(
            new ContainersListParameters()
            { 
                All = true
            });

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

        public static async Task<List<DockerImage>> GetImages()
        {
            List<DockerImage> images = new List<DockerImage>();

            IList<ImagesListResponse> responses = await DockerClient.Images.ListImagesAsync(new ImagesListParameters()
            {
                All = true
            });

            foreach(ImagesListResponse res in responses)
            {
                DockerImage image = new DockerImage()
                {
                    ID = res.ID,
                    Containers = res.Containers,
                    Created = res.Created,
                    Labels = res.Labels,
                    ParentID = res.ParentID,
                    RepoDigests = res.RepoDigests,
                    RepoTags = res.RepoTags,
                    SharedSize = res.SharedSize,
                    Size = res.Size,
                    VirtualSize = res.VirtualSize
                };

                images.Add(image);
            }

            return images;
        }
    }
}
