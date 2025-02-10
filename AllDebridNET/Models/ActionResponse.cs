using System.Text.Json.Serialization;

namespace AllDebridNET;

public class ActionResponse
{
    /// <summary>
    ///     The status of the action.
    /// </summary>
    [JsonPropertyName("message")]
    public String? Message { get; set; }
}