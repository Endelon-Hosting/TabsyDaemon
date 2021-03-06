using Docker.DotNet;
using Docker.DotNet.Models;

using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

using TabsyDaemon.Docker;
using TabsyDaemon.Services;
using System;
using TabsyDaemon.Logging;
using static System.Net.Mime.MediaTypeNames;

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

            foreach (ContainerListResponse clr in containers)
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

            foreach (ImagesListResponse res in responses)
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
        public static async Task DownloadImage(string name)
        {
            bool b = false;

            foreach (DockerImage image in GetImages().Result)
            {
                if (image.Name == name)
                    b = true;
            }
            if (b)
                return;

            Logger.Info($"Attempting to pull image {name} to {DockerClient.Configuration.EndpointBaseUri}");

            try
            {
                var report = new Progress<JSONMessage>(msg =>
                {
                    Logger.Info($"{msg.Status}|{msg.ProgressMessage}|{msg.ErrorMessage}");
                });

                await DockerClient.Images.CreateImageAsync(new ImagesCreateParameters
                {
                    FromImage = name
                },
                new AuthConfig(),
                report
                );
                Logger.Info($"Successfully pulled image {name} to {DockerClient.Configuration.EndpointBaseUri}");
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception thrown attempting to pull image {name} to {DockerClient.Configuration.EndpointBaseUri}");
                Logger.Error($"{ex}");
            }
        }
        public static async Task CreateContainer(string name, string dir, string image, string containerdir, string hostdir, long cpu, long memory)
        {
            try
            {
                await DownloadImage(image);

                List<string> entry = new List<string>();

                entry.Add("/bin/bash");
                entry.Add("/root/startup.sh");

                HostConfig hostConfig = new HostConfig()
                {
                    CPUPercent = cpu,
                    Memory = memory,
                    Binds = new[] {
                        hostdir + ":" + containerdir
                    }
                };

                CreateContainerResponse response = await DockerClient.Containers.CreateContainerAsync(new CreateContainerParameters()
                {
                    Name = name,
                    User = "root",
                    WorkingDir = dir,
                    Image = image,
                    HostConfig = hostConfig,
                    Entrypoint = entry,
                },
                CancellationToken.None);

                foreach (string s in response.Warnings)
                {
                    Logger.Warn($"Waring creating docker container: {s}");
                }
            }
            catch (Exception e)
            {
                Logger.Error($"Error creating docker container: {e.Message}");
            }
        }
        public static async Task StartContainer(string name)
        {

        }

        public static async Task CreateVolume(string name, string containerdir, string hostdir)
        {
            Dictionary<string, string> thing = new Dictionary<string, string>();

            thing.Add(containerdir, hostdir);

            VolumeResponse response = await DockerClient.Volumes.CreateAsync(new VolumesCreateParameters()
            {
                Name = name,
                DriverOpts = thing
            },
            CancellationToken.None);
        }
    }
}
