using System.Text.Json.Serialization;

namespace AllDebridNET;

internal class UserLinks
{
    [JsonPropertyName("links")]
    [JsonConverter(typeof(ListConverter<UserLink>))]
    public List<UserLink> Links { get; set; } = [];
}

public class UserLink
{
    /// <summary>
    ///     Link URL.
    /// </summary>
    [JsonPropertyName("link")]
    public String? Link { get; set; }

    /// <summary>
    ///     Link file name.
    /// </summary>
    [JsonPropertyName("filename")]
    public String? Filename { get; set; }

    /// <summary>
    ///     Link file size.
    /// </summary>
    [JsonPropertyName("size")]
    public Int64 Size { get; set; }

    /// <summary>
    ///     When the link was saved.
    /// </summary>
    [JsonPropertyName("date")]
    public Int64 Date { get; set; }

    /// <summary>
    ///     Link host.
    /// </summary>
    [JsonPropertyName("host")]
    public String? Host { get; set; }
}