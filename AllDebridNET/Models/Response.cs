using System.Text.Json.Serialization;

namespace AllDebridNET;

internal class Response<T>
{
    [JsonPropertyName("status")]
    public String? Status { get; init; }

    [JsonPropertyName("data")]
    public T? Data { get; init; }

    [JsonPropertyName("error")]
    public ResponseError? Error { get; init; }
}

public class ResponseError
{
    [JsonPropertyName("code")]
    public String? Code { get; init; }

    [JsonPropertyName("message")]
    public String? Message { get; init; }
}