using System.Text.Json.Serialization;

namespace AllDebridNET;

public class HostDomains
{
    [JsonPropertyName("hosts")]
    public List<String>? Hosts { get; set; }

    [JsonPropertyName("streams")]
    public List<String>? Streams { get; set; }

    [JsonPropertyName("redirectors")]
    public List<String>? Redirectors { get; set; }
}