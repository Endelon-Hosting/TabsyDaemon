using System;

using AustinHarris.JsonRpc;

namespace TabsyClient.Rpc // Client
{
    public class RpcHandler : JsonRpcService
    {
        [JsonRpcMethod("network.pong")]
        private void Pong(DateTime dt)
        {
            Console.WriteLine("Pong");
            Console.WriteLine($"Took {(DateTime.Now - dt).TotalSeconds}");
        }

        [JsonRpcMethod("docker.containers.count.response")]
        private void SetDockerContainersCount(int c)
        {
            Console.WriteLine($"Docker {c} conatiners found on daemon");
        }
        [JsonRpcMethod("docker.images.count.response")]
        private void SetDockerImagesCount(int c)
        {
            Console.WriteLine($"Docker {c} images found on daemon");
        }
    }
}
