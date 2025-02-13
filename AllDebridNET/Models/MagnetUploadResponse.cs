using System.Text.Json.Serialization;

namespace AllDebridNET;

internal class MagnetUploadResponse
{
    [JsonPropertyName("magnets")]
    [JsonConverter(typeof(ListConverter<MagnetAddResult>))]
    public List<MagnetAddResult>? Magnets { get; set; }

    [JsonPropertyName("files")]
    [JsonConverter(typeof(ListConverter<MagnetAddResult>))]
    public List<MagnetAddResult>? Files { get; set; }
}