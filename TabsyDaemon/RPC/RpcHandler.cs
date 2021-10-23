using System;

using AustinHarris.JsonRpc;

namespace TabsyDaemon.Rpc // Server
{
    public class RpcHandler : JsonRpcService
    {
        [JsonRpcMethod]
        public void Ping(DateTime dt)
        {
            RpcClient.TriggerRpc("Pong", dt);
        }
    }
}
