using System;

using AustinHarris.JsonRpc;

namespace TabsyClient.Rpc
{
    public class RpcHandler : JsonRpcService
    {
        [JsonRpcMethod]
        private void Pong()
        {
            Console.WriteLine("Pong");
        }
    }
}
