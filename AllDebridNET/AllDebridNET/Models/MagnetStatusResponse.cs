using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AllDebridNET
{
    internal class MagnetStatusResponse
    {
        [JsonProperty("magnets")]
        public IList<Magnet> Magnets { get; set; }

        [JsonProperty("counter")]
        public Int64 Counter { get; set; }

        [JsonProperty("fullsync")]
        public Boolean Fullsync { get; set; }
    }
}
