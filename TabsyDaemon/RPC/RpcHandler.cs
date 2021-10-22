﻿using AustinHarris.JsonRpc;

namespace TabsyDaemon.RPC
{
    public class RpcHandler : JsonRpcService
    {
        [JsonRpcMethod]
        public int Sum(int x, int y)
        {
            return x + y;
        }
    }
}
