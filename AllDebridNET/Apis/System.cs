namespace AllDebridNET;

public class SystemApi
{
    private readonly Requests _requests;

    internal SystemApi(HttpClient httpClient, Store store)
    {
        _requests = new(httpClient, store);
    }

    /// <summary>
    ///     Ping the service.
    /// </summary>
    /// <param name="cancellationToken"></param>
    public async Task<PingResponse> PingAsync(CancellationToken cancellationToken = default)
    {
        return await _requests.GetRequestAsync<PingResponse>("ping", false, null, cancellationToken);
    }
}