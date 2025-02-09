using System.Text.Json.Serialization;

namespace AllDebridNET;

public class DelayedLink
{
    /// <summary>
    ///     Current status.
    ///     1: Still processing.
    ///     2: Download link is available.
    ///     3: Error, could not generate download link. 
    /// </summary>
    [JsonPropertyName("status")]
    public Int64 Status { get; set; }

    /// <summary>
    ///     Estimated time left to wait.
    /// </summary>
    [JsonPropertyName("time_left")]
    public Int64 TimeLeft { get; set; }

    /// <summary>
    ///    Download link, available when it is ready.
    /// </summary>
    [JsonPropertyName("link")]
    public String? Link { get; set; }
}