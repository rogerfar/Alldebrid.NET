using Newtonsoft.Json;

namespace AllDebridNET;

public class LinksRedirectorResponse
{
    [JsonProperty("links")]
    public IList<String>? Links { get; set; }
}