using System.Text.Json.Serialization;

namespace AllDebridNET;

public class DownloadLink
{
    /// <summary>
    ///     Requested link, simplified if it was not in canonical form.
    /// </summary>
    [JsonPropertyName("link")]
    public String? Link { get; set; }

    /// <summary>
    ///     Link host minified.
    /// </summary>
    [JsonPropertyName("host")]
    public String? Host { get; set; }

    /// <summary>
    ///     Link's file filename.
    /// </summary>
    [JsonPropertyName("filename")]
    public String? Filename { get; set; }

    /// <summary>
    ///     Unused.
    /// </summary>
    [JsonPropertyName("paws")]
    public Boolean Paws { get; set; }

    /// <summary>
    ///     Filesize of the link's file.
    /// </summary>
    [JsonPropertyName("filesize")]
    public Int64 Filesize { get; set; }

    /// <summary>
    ///     List of alternative links with other resolutions for some video links.
    /// </summary>
    [JsonPropertyName("streams")]
    [JsonConverter(typeof(ListConverter<DownloadLinkStream>))]
    public List<DownloadLinkStream>? Streams { get; set; }

    /// <summary>
    ///     Generation ID.
    /// </summary>
    [JsonPropertyName("id")]
    public String? Id { get; set; }

    /// <summary>
    ///     Matched host main domain.
    /// </summary>
    [JsonPropertyName("hostDomain")]
    public String? HostDomain { get; set; }

    /// <summary>
    ///     Delayed ID if link need time to generate.
    /// </summary>
    public Int64? Delayed { get; set; }
}

public class DownloadLinkStream
{
    [JsonPropertyName("quality")]
    public String? Quality { get; set; }

    [JsonPropertyName("ext")]
    public String? Ext { get; set; }

    [JsonPropertyName("filesize")]
    public Int64 Filesize { get; set; }

    [JsonPropertyName("name")]
    public String? Name { get; set; }

    [JsonPropertyName("link")]
    public String? Link { get; set; }

    [JsonPropertyName("id")]
    public String? Id { get; set; }
}