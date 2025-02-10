using System.Text.Json.Serialization;

namespace AllDebridNET;

public class Hosts
{
    [JsonPropertyName("hosts")]
    public Dictionary<String, Host>? HostList { get; set; }

    [JsonPropertyName("streams")]
    public Dictionary<String, Host>? Streams { get; set; }

    [JsonPropertyName("redirectors")]
    public Dictionary<String, Host>? Redirectors { get; set; }
}

public class Host
{
    /// <summary>
    ///     Host name.
    /// </summary>
    [JsonPropertyName("name")]
    public String? Name { get; set; }

    /// <summary>
    ///     Either "premium" or "free". Premium hosts need a premium subscription.
    /// </summary>
    [JsonPropertyName("type")]
    public String? Type { get; set; }

    /// <summary>
    ///     Host domains.
    /// </summary>
    [JsonPropertyName("domains")]
    public List<String>? Domains { get; set; }

    /// <summary>
    ///     Rgexp matching link format that Alldebrid supports.
    /// </summary>
    [JsonPropertyName("regexps")]
    public List<String>? Regexps { get; set; }

    [JsonPropertyName("regexp")]
    [JsonConverter(typeof(RegexpConverter))]
    public List<String>? Regexp { get; set; }

    /// <summary>
    ///     Is the host currently (less than 5 min) working on Alldebrid (only tested on some hosts, updated every ~ 10 min).
    /// </summary>
    [JsonPropertyName("status")]
    public Boolean? Status { get; set; }

    [JsonPropertyName("hardRedirect")]
    public List<String>? HardRedirect { get; set; }
}

internal class HostsPriorityResponse
{
    [JsonPropertyName("hosts")]
    public Dictionary<String, Int64>? Hosts { get; set; }
}