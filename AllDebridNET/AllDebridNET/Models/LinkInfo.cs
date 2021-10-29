using System;
using Newtonsoft.Json;

namespace AllDebridNET
{
    public class LinkInfo
    {
        [JsonProperty("link")]
        public String Link { get; set; }

        [JsonProperty("filename")]
        public String Filename { get; set; }

        [JsonProperty("size")]
        public Int64 Size { get; set; }

        [JsonProperty("host")]
        public String Host { get; set; }

        [JsonProperty("hostDomain")]
        public String HostDomain { get; set; }
    }
}
