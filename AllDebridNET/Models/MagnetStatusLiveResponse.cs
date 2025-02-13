using System.Text.Json.Serialization;

namespace AllDebridNET;

public class MagnetStatusLiveResponse
{
    /// <summary>
    ///    List of magnets.
    /// </summary>
    [JsonPropertyName("magnets")]
    [JsonConverter(typeof(ListConverter<Magnet>))]
    public List<Magnet>? Magnets { get; set; }

    /// <summary>
    ///    Counter to use on the next call.
    /// </summary>
    [JsonPropertyName("counter")]
    public Int64 Counter { get; set; }

    /// <summary>
    ///    If returned to true, the response is the start of a new session, all data was returned and a new counter was set.
    /// </summary>
    [JsonPropertyName("fullsync")]
    public Boolean? Fullsync { get; set; }
}