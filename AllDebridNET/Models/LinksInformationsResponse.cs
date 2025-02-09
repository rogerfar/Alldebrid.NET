using System.Text.Json.Serialization;

namespace AllDebridNET;

internal class LinksInformationsResponse
{
    [JsonPropertyName("infos")]
    public List<LinkInfo>? Infos { get; set; }
}