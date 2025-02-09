using System.Text.Json.Serialization;

namespace AllDebridNET;

internal class LinksRedirectorResponse
{
    [JsonPropertyName("links")]
    public List<String>? Links { get; set; }
}