using Newtonsoft.Json;

namespace AllDebridNET;

public class MagnetAddResult
{
    [JsonProperty("magnet")]
    public String? Magnet { get; set; }

    [JsonProperty("file")]
    public String? File { get; set; }

    [JsonProperty("hash")]
    public String? Hash { get; set; }

    [JsonProperty("name")]
    public String? Name { get; set; }

    [JsonProperty("size")]
    public Int64? Size { get; set; }

    [JsonProperty("ready")]
    public Boolean? Ready { get; set; }

    [JsonProperty("id")]
    public Int64? Id { get; set; }

    [JsonProperty("error")]
    public ResponseError? Error { get; set; }
}