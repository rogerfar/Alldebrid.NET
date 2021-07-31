using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AllDebridNET
{
    public class HostDomains
    {
        [JsonProperty("hosts")]
        public List<String> Hosts { get; set; }

        [JsonProperty("streams")]
        public List<String> Streams { get; set; }

        [JsonProperty("redirectors")]
        public List<String> Redirectors { get; set; }
    }
}
