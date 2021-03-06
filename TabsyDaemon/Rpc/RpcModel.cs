using Newtonsoft.Json;

namespace TabsyDaemon.Rpc
{
    public class RpcModel
    {
        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("params")]
        public object[] Params { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }
    }
}
