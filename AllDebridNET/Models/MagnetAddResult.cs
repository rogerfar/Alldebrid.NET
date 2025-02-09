using System.Text.Json.Serialization;

namespace AllDebridNET;

public class MagnetAddResult
{
    /// <summary>
    ///     Magnet sent.
    /// </summary>
    [JsonPropertyName("magnet")]
    public String? Magnet { get; set; }

    /// <summary>
    ///     Magnet hash.
    /// </summary>
    [JsonPropertyName("hash")]
    public String? Hash { get; set; }

    /// <summary>
    ///     Magnet filename, or 'noname' if could not parse it.
    /// </summary>
    [JsonPropertyName("name")]
    public String? Name { get; set; }

    /// <summary>
    ///     Magnet files size.
    /// </summary>
    [JsonPropertyName("size")]
    public Int64? Size { get; set; }

    /// <summary>
    ///     Whether the magnet is already available.
    /// </summary>
    [JsonPropertyName("ready")]
    public Boolean? Ready { get; set; }

    /// <summary>
    ///     Magnet id, used to query status.
    /// </summary>
    [JsonPropertyName("id")]
    public Int64? Id { get; set; }

    [JsonPropertyName("error")]
    public ResponseError? Error { get; set; }
}