using System.Text.Json.Serialization;

namespace AllDebridNET;

public class PinRequest
{
    /// <summary>
    ///     Pin code to display to your user.
    /// </summary>
    [JsonPropertyName("pin")]
    public String? Pin { get; set; }

    /// <summary>
    ///     Hash needed for the pin/check call.
    /// </summary>
    [JsonPropertyName("check")]
    public String? Check { get; set; }

    /// <summary>
    ///     Number of second before the code expires.
    /// </summary>
    [JsonPropertyName("expires_in")]
    public Int64 ExpiresIn { get; set; }

    /// <summary>
    ///     Url to display with PIN included.
    /// </summary>
    [JsonPropertyName("user_url")]
    public String? UserUrl { get; set; }

    /// <summary>
    ///     Base url.
    /// </summary>
    [JsonPropertyName("base_url")]
    public String? BaseUrl { get; set; }
}