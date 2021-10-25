using System.Collections.Generic;
using System.IO;

using TabsyDaemon.Models;

namespace TabsyDaemon.Controller
{
    public class ServerController
    {
        public static void Create(string name, long cpu, long memory, TabsyImage image, string[] freePorts)
        {
            Directory.CreateDirectory(Environment.ContainerDirectory + $"/{name}");
            DockerController.CreateContainer(name, "/", image.Image, "/root", Environment.ContainerDirectory + $"/{name}", cpu, memory * 1024 * 1024).Wait();

            List<string> startupLines = new List<string>();

            foreach(string line in image.Install)
            {
                string l = line;

                l = l.Replace("{CPU}", cpu.ToString());
                l = l.Replace("{RAM}", memory.ToString());

                int i = 0;

                foreach(string port in )

                startupLines.Add(l);
            }
        }
    }
}