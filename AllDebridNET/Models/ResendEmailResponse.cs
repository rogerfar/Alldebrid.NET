using System.Text.Json.Serialization;

namespace AllDebridNET;

public class ResendEmailResponse
{
    /// <summary>
    ///     Email was sent again
    /// </summary>
    [JsonPropertyName("sent")]
    public Boolean Sent { get; set; }
}
