using System.Text.Json.Serialization;

namespace AllDebridNET;

internal class MagnetsStatusResponse
{
    /// <summary>
    ///     List of magnets.
    /// </summary>
    [JsonPropertyName("magnets")]
    [JsonConverter(typeof(ListConverter<Magnet>))]
    public List<Magnet>? Magnets { get; set; }
}