using System.Text.Json.Serialization;

namespace AllDebridNET;

internal class LinksInformationsResponse
{
    [JsonPropertyName("infos")]
    [JsonConverter(typeof(ListConverter<LinkInfo>))]
    public List<LinkInfo>? Infos { get; set; }
}