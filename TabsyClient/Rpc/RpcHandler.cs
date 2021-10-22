using AustinHarris.JsonRpc;

using TabsyClient.Logging;

namespace TabsyClient.Rpc
{
    public class RpcHandler : JsonRpcService
    {
        [JsonRpcMethod]
        private void log(string s)
        {
            Logger.Debug(s);
        }
    }
}
