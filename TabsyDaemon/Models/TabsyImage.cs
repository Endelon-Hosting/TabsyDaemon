namespace TabsyDaemon.Models
{

    using Newtonsoft.Json;

    public partial class TabsyImage
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("startup")]
        public string[] Startup { get; set; }

        [JsonProperty("stop")]
        public string Stop { get; set; }

        [JsonProperty("ports")]
        public long[] Ports { get; set; }

        [JsonProperty("install")]
        public string[] Install { get; set; }

        [JsonProperty("files")]
        public string[] Files { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }
    }
}
