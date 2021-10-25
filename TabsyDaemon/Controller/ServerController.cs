using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabsyDaemon.Controller
{
    public class ServerController
    {
        public static void Create(string name, string image, string containerdir, long cpu, long memory, List<string> envs)
        {
            Directory.CreateDirectory(Environment.TabsyContainerDirectory + $"/{name}");
            DockerController.CreateContainer(name, "/", image, containerdir, Environment.TabsyContainerDirectory + $"/{name}", cpu, memory * 1024 * 1024, envs).Wait();
        }
    }
}
