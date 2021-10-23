using AustinHarris.JsonRpc;

namespace TabsyDaemon.Rpc
{
    public class RpcHandler : JsonRpcService
    {
        [JsonRpcMethod]
        public void Ping()
        {
            RpcClient.TriggerRpc("Pong");
        }
    }
}
