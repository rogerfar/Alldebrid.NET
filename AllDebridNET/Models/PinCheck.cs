using System.Text.Json.Serialization;

namespace AllDebridNET;

public class PinCheck
{
    /// <summary>
    ///     Auth apikey, available once user has submitted the pin code.
    /// </summary>
    [JsonPropertyName("apikey")]
    public String? Apikey { get; set; }

    /// <summary>
    ///     false if user didn't enter the pin on website yet.
    /// </summary>
    [JsonPropertyName("activated")]
    public Boolean Activated { get; set; }

    /// <summary>
    ///     Seconds left before PIN expires
    /// </summary>
    [JsonPropertyName("expires_in")]
    public Int64 ExpiresIn { get; set; }
}