using AustinHarris.JsonRpc;

namespace TabsyDaemon.RPC
{
    public class RpcHandler : JsonRpcService
    {
        [JsonRpcMethod]
        public int Sum(int x, int y)
        {
            return x + y;
        }

        [JsonRpcMethod] // handles JsonRpc like : {'method':'incr','params':[5],'id':1}
        private int incr(int i) { return i + 1; }
    }
}
