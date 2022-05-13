using Newtonsoft.Json;

namespace AllDebridNET;

internal class Response<T>
{
    [JsonProperty("status")]
    public String? Status { get;set; }

    [JsonProperty("data")]
    public T? Data { get;set; }

    [JsonProperty("error")]
    public ResponseError? Error { get;set; }
}

public class ResponseError
{
    [JsonProperty("code")]
    public String? Code { get;set; }

    [JsonProperty("message")]
    public String? Message { get;set; }
}