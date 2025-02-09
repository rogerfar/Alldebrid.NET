using System.Text.Json.Serialization;
using AllDebridNET.Converters;

namespace AllDebridNET;

public class LinkInfo
{
    /// <summary>
    ///     Requested link.
    /// </summary>
    [JsonPropertyName("link")]
    public String? Link { get; set; }

    /// <summary>
    ///     Link's file filename.
    /// </summary>
    [JsonPropertyName("filename")]
    public String? Filename { get; set; }

    /// <summary>
    ///     Link's file size in bytes.
    /// </summary>
    [JsonPropertyName("size")]
    [JsonConverter(typeof(StringToInt64Converter))]
    public Int64 Size { get; set; }

    /// <summary>
    ///     Link host.
    /// </summary>
    [JsonPropertyName("host")]
    public String? Host { get; set; }

    /// <summary>
    ///     Host main domain.
    /// </summary>
    [JsonPropertyName("hostDomain")]
    public String? HostDomain { get; set; }

    /// <summary>
    ///     Link error.
    /// </summary>
    [JsonPropertyName("error")]
    public ResponseError? Error { get; set; }
}