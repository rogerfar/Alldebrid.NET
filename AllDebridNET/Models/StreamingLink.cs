using Newtonsoft.Json;

namespace AllDebridNET;

public class StreamingLink
{
    /// <summary>
    /// Optional. Download link, ONLY if available. This attribute WONT BE SET if download link is a delayed link.
    /// </summary>
    [JsonProperty("link")]
    public String? Link { get; set; }

    [JsonProperty("host")]
    public String? Host { get; set; }

    /// <summary>
    /// Link's file filename.
    /// </summary>
    [JsonProperty("filename")]
    public String? Filename { get; set; }

    /// <summary>
    /// Filesize of the link's file.
    /// </summary>
    [JsonProperty("filesize")]
    public Int64 Filesize { get; set; }

    [JsonProperty("id")]
    public String? Id { get; set; }

    [JsonProperty("streams")]
    public List<StreamingLinkStream>? Streams { get; set; }

    /// <summary>
    ///     Optional. Delayed ID to get download link with delayed link flow.
    /// </summary>
    public Int64? Delayed { get; set; }
}

public class StreamingLinkStream
{
    [JsonProperty("id")]
    public String? Id { get; set; }

    [JsonProperty("ext")]
    public String? Ext { get; set; }

    [JsonProperty("quality")]
    public String? Quality { get; set; }

    [JsonProperty("filesize")]
    public Int64 Filesize { get; set; }

    [JsonProperty("proto")]
    public String? Proto { get; set; }

    [JsonProperty("name")]
    public String? Name { get; set; }

    [JsonProperty("tb")]
    public Decimal? Tb { get; set; }

    [JsonProperty("abr")]
    public Int64? Abr { get; set; }
}