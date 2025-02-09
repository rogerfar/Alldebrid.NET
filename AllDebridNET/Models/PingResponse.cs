using System.Text.Json.Serialization;

namespace AllDebridNET;

public class PingResponse
{
    /// <summary>
    ///     Pong!
    /// </summary>
    [JsonPropertyName("ping")]
    public String? Ping { get; set; }

    /// <summary>
    ///     The version of the API.
    /// </summary>
    [JsonPropertyName("version")]
    public String? Version { get; set; }

    /// <summary>
    ///     Indicates if the version used is a subversion.
    /// </summary>
    [JsonPropertyName("subversion")]
    public Boolean? Subversion { get; set; }
}