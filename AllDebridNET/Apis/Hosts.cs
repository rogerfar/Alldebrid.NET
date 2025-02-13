namespace AllDebridNET;

public interface IHostsApi
{
    /// <summary>
    ///     Use this endpoint to retrieve informations about what hosts we support and all related informations about it.
    ///     This request does not require authentication.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    Task<Hosts> GetHostsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Use this endpoint to only retrieve the list of supported hosts domains and redirectors as an array. This will also include
    ///     any alternative domain the hosts or redirectors have. Please use regexps availables in /hosts or /user/hosts endpoints to validate supported links.
    ///     This request does not require authentication.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    Task<HostDomains> GetDomainsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Not all hosts are created equal, so some hosts are more limited than other.
    ///     Use this endpoint to retrieve an ordered list of main domain of hosts, from more open to more restricted.
    ///     This request does not require authentication.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    Task<Dictionary<String, Int64>> GetHostsPriorityAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     This endpoint retrieves a complete list of all available hosts for this user. Depending of the account subscription status
    ///     (free user, trial mode, premium user), the list and limitations will vary.
    ///     The limits and quota are updated in real time. Use this page to have an up-to-date list of service the user can use on Alldebrid.
    ///     Quotas will reset every day for premium users.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    Task<Hosts> GetHostsForUserAsync(CancellationToken cancellationToken = default);
}

public class HostsApi : IHostsApi
{
    private readonly Requests _requests;

    internal HostsApi(HttpClient httpClient, Store store)
    {
        _requests = new(httpClient, store);
    }
        
   /// <inheritdoc />
    public async Task<Hosts> GetHostsAsync(CancellationToken cancellationToken = default)
    {
        return await _requests.GetRequestAsync<Hosts>("hosts", false, null, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<HostDomains> GetDomainsAsync(CancellationToken cancellationToken = default)
    {
        return await _requests.GetRequestAsync<HostDomains>($"hosts/domains", false, null, cancellationToken);
    }
        
    /// <inheritdoc />
    public async Task<Dictionary<String, Int64>> GetHostsPriorityAsync(CancellationToken cancellationToken = default)
    {
        var response = await _requests.GetRequestAsync<HostsPriorityResponse>($"hosts/priority", false, null, cancellationToken);

        return response.Hosts ?? [];
    }

    /// <inheritdoc />
    public async Task<Hosts> GetHostsForUserAsync(CancellationToken cancellationToken = default)
    {
        return await _requests.GetRequestAsync<Hosts>($"user/hosts", true, null, cancellationToken);
    }
}