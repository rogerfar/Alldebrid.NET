using System.Text.Json.Serialization;

namespace AllDebridNET;

internal class MagnetUploadResponse
{
    [JsonPropertyName("magnets")]
    public List<MagnetAddResult>? Magnets { get; set; }

    [JsonPropertyName("files")]
    public List<MagnetAddResult>? Files { get; set; }
}