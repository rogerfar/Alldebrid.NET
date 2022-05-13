using Newtonsoft.Json;

namespace AllDebridNET;

public class DelayedLink
{
    [JsonProperty("status")]
    public Int64 Status { get; set; }

    [JsonProperty("time_left")]
    public Int64 TimeLeft { get; set; }

    [JsonProperty("link")]
    public String? Link { get; set; }
}