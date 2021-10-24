using System;

using AustinHarris.JsonRpc;

using TabsyDaemon.Controller;

namespace TabsyDaemon.Rpc // Server
{
    public class RpcHandler : JsonRpcService
    {
        [JsonRpcMethod("network.ping")]
        private void Ping(DateTime dt)
        {
            RpcClient.TriggerRpc("network.pong", dt);
        }
        [JsonRpcMethod("docker.containers.count.request")]
        private void GetContainersCount()
        {
            RpcClient.TriggerRpc("docker.containers.count.response", DockerController.GetContainers().Result.Count);
        }
        [JsonRpcMethod("docker.images.count.request")]
        private void GetImagesCount()
        {
            RpcClient.TriggerRpc("docker.images.count.response", DockerController.GetImages().Result.Count);
        }
    }
}
