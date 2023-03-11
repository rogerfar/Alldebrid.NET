namespace AllDebridNET;

public class AuthenticationApi
{
    private readonly Store _store;
    private readonly Requests _requests;

    internal AuthenticationApi(HttpClient httpClient, Store store)
    {
        _store = store;
        _requests = new Requests(httpClient, store);
    }

    /// <summary>
    ///     Get the pin authentication object.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    /// <returns>
    ///     Redirect the user to the resulting "BaseUrl" or "UserUrl" then use CheckPin to verify if the user completed entering the pin.
    /// </returns>
    public async Task<PinRequest> GetPinAsync(CancellationToken cancellationToken = default)
    {
        return await _requests.GetRequestAsync<PinRequest>("pin/get", false, null, cancellationToken);
    }

    /// <summary>
    ///     The endpoint where the auth apikey will be available after the user submitted the PIN code on the Alldebrid website.
    ///     The endpoint is available for 10 minutes after the PIN code is generated.
    /// 
    ///     You should poll on the endpoint until the user has submitted the PIN code and an auth apikey is returned, or until the endpoint expires after 600 seconds.
    /// </summary>
    /// <param name="pinCheck">
    ///     Random hash from check_url given in /pin/get.
    /// </param>
    /// <param name="pin">
    ///     Pin code from check_url given in /pin/get.
    /// </param>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    public async Task<PinCheck> CheckPinAsync(String pinCheck, String pin, CancellationToken cancellationToken = default)
    {
        var parameters = new Dictionary<String, String>
        {
            { "check", pinCheck },
            { "pin", pin }
        };

        var result = await _requests.GetRequestAsync<PinCheck>($"pin/check", false, parameters, cancellationToken);

        if (result.Apikey != null)
        {
            _store.ApiKey = result.Apikey;
        }

        return result;
    }
}