using System.Text.Json.Serialization;

namespace AllDebridNET;

internal class MagnetStatusResponse
{
    [JsonPropertyName("magnets")]
    [JsonConverter(typeof(ListConverter<Magnet>))]
    public List<Magnet>? Magnets { get; set; }
}