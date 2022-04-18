using Newtonsoft.Json;

namespace AllDebridNET;

internal class MagnetStatusResponse
{
    [JsonProperty("magnets")]
    public Magnet Magnets { get; set; }
}