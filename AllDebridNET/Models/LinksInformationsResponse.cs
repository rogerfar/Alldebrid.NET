using Newtonsoft.Json;

namespace AllDebridNET;

public class LinksInformationsResponse
{
    [JsonProperty("infos")]
    public IList<LinkInfo>? Infos { get; set; }
}