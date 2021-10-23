using Docker.DotNet.Models;

using Newtonsoft.Json;

using System.Reflection;

using TabsyDaemon.Services;

namespace TabsyDaemon.Rpc
{
    public class RpcClient
    {
        public static void TriggerRpc(string name, params object[] parameter)
        {
            RpcModel m = new RpcModel()
            {
                Method = name,
                Params = parameter
            };

            RpcService.Instance.TriggerRpc(m);
        }
    }
}
