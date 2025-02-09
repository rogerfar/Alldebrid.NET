using System.Text.Json.Serialization;

namespace AllDebridNET;

internal class MagnetStatusResponse
{
    [JsonPropertyName("magnets")]
    public List<Magnet>? Magnets { get; set; }
}