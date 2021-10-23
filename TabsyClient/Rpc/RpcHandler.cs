using System;

using AustinHarris.JsonRpc;

namespace TabsyClient.Rpc // Client
{
    public class RpcHandler : JsonRpcService
    {
        [JsonRpcMethod]
        private void Pong(DateTime dt)
        {
            Console.WriteLine("Pong");
            Console.WriteLine($"Took {(DateTime.Now - dt).TotalSeconds}");
        }
    }
}
