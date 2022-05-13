using Newtonsoft.Json;

namespace AllDebridNET;

public class DownloadLink
{
    /// <summary>
    ///     Requested link, simplified if it was not in canonical form.
    /// </summary>
    [JsonProperty("link")]
    public String? Link { get; set; }

    /// <summary>
    ///     Link host minified.
    /// </summary>
    [JsonProperty("host")]
    public String? Host { get; set; }

    /// <summary>
    ///     Link's file filename.
    /// </summary>
    [JsonProperty("filename")]
    public String? Filename { get; set; }

    /// <summary>
    ///     Unused.
    /// </summary>
    [JsonProperty("paws")]
    public Boolean Paws { get; set; }

    /// <summary>
    ///     Filesize of the link's file.
    /// </summary>
    [JsonProperty("filesize")]
    public Int64 Filesize { get; set; }

    /// <summary>
    ///     List of alternative links with other resolutions for some video links.
    /// </summary>
    [JsonProperty("streams")]
    public List<DownloadLinkStream>? Streams { get; set; }

    /// <summary>
    ///     Generation ID.
    /// </summary>
    [JsonProperty("id")]
    public String? Id { get; set; }

    /// <summary>
    ///     Matched host main domain.
    /// </summary>
    [JsonProperty("hostDomain")]
    public String? HostDomain { get; set; }

    /// <summary>
    ///     Delayed ID if link need time to generate.
    /// </summary>
    public Int64? Delayed { get; set; }
}

public class DownloadLinkStream
{
    [JsonProperty("quality")]
    public String? Quality { get; set; }

    [JsonProperty("ext")]
    public String? Ext { get; set; }

    [JsonProperty("filesize")]
    public Int64 Filesize { get; set; }

    [JsonProperty("name")]
    public String? Name { get; set; }

    [JsonProperty("link")]
    public String? Link { get; set; }

    [JsonProperty("id")]
    public String? Id { get; set; }
}