namespace AllDebridNET;

public interface ISystemApi
{
    /// <summary>
    ///     Ping the service.
    /// </summary>
    /// <param name="cancellationToken"></param>
    Task<PingResponse> PingAsync(CancellationToken cancellationToken = default);
}

public class SystemApi : ISystemApi
{
    private readonly Requests _requests;

    internal SystemApi(HttpClient httpClient, Store store)
    {
        _requests = new(httpClient, store);
    }

    /// <inheritdoc />
    public async Task<PingResponse> PingAsync(CancellationToken cancellationToken = default)
    {
        return await _requests.GetRequestAsync<PingResponse>("ping", false, null, cancellationToken);
    }
}