using System.Text.Json.Serialization;

namespace AllDebridNET;

public class StreamingLink
{
    /// <summary>
    /// Optional. Download link, ONLY if available. This attribute WONT BE SET if download link is a delayed link.
    /// </summary>
    [JsonPropertyName("link")]
    public String? Link { get; set; }

    [JsonPropertyName("host")]
    public String? Host { get; set; }

    /// <summary>
    /// Link's file filename.
    /// </summary>
    [JsonPropertyName("filename")]
    public String? Filename { get; set; }

    /// <summary>
    /// Filesize of the link's file.
    /// </summary>
    [JsonPropertyName("filesize")]
    public Int64 Filesize { get; set; }

    [JsonPropertyName("id")]
    public String? Id { get; set; }

    [JsonPropertyName("streams")]
    [JsonConverter(typeof(ListConverter<StreamingLinkStream>))]
    public List<StreamingLinkStream>? Streams { get; set; }

    /// <summary>
    ///     Optional. Delayed ID to get download link with delayed link flow.
    /// </summary>
    public Int64? Delayed { get; set; }
}

public class StreamingLinkStream
{
    [JsonPropertyName("id")]
    public String? Id { get; set; }

    [JsonPropertyName("ext")]
    public String? Ext { get; set; }

    [JsonPropertyName("quality")]
    public String? Quality { get; set; }

    [JsonPropertyName("filesize")]
    public Int64 Filesize { get; set; }

    [JsonPropertyName("proto")]
    public String? Proto { get; set; }

    [JsonPropertyName("name")]
    public String? Name { get; set; }

    [JsonPropertyName("tb")]
    public Decimal? Tb { get; set; }

    [JsonPropertyName("abr")]
    public Int64? Abr { get; set; }
}