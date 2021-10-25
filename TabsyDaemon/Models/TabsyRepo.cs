namespace TabsyDaemon.Models
{
    using Newtonsoft.Json;

    public partial class TabsyRepo
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("version")]
        public long Version { get; set; }

        [JsonProperty("images")]
        public string[] Images { get; set; }
    }
}
