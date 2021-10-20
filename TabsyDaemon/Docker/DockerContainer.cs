using Docker.DotNet.Models;

using System;
using System.Collections.Generic;

namespace TabsyDaemon.Docker
{
    public class DockerContainer
    {
        public string ID { get; set; }
        public IList<string> Names { get; set; }
        public string Image { get; set; }
        public string ImageID { get; set; }
        public string Command { get; set; }
        public DateTime Created { get; set; }
        public IList<Port> Ports { get; set; }
        public long SizeRw { get; set; }
        public long SizeRootFs { get; set; }
        public IDictionary<string, string> Labels { get; set; }
        public string State { get; set; }
        public string Status { get; set; }
        public SummaryNetworkSettings NetworkSettings { get; set; }
        public IList<MountPoint> Mounts { get; set; }
    }
}
