﻿namespace AllDebridNET;

public interface IAllDebridNETClient
{
    IAuthenticationApi Authentication { get; }
    IHostsApi Hosts { get; }
    ILinksApi Links { get; }
    IMagnetApi Magnet { get; }
    ISystemApi System { get; set; }
    IUserApi User { get; }
    IUserLinksApi UserLink { get; set; }
}

/// <summary>
///     The AllDebridNET consumes the alldebrid.com API.
///     Documentation about the API can be found here: https://docs.alldebrid.com/
/// </summary>
public class AllDebridNETClient : IAllDebridNETClient
{
    private readonly Store _store = new();

    public IAuthenticationApi Authentication { get; }
    public IHostsApi Hosts { get; }
    public ILinksApi Links { get; }
    public IMagnetApi Magnet { get; }
    public ISystemApi System { get; set; }
    public IUserApi User { get; }
    public IUserLinksApi UserLink { get; set; }

    /// <summary>
    ///     Initialize the AllDebridNET API.
    ///     To use authentication provide the key for your user. If the user doesn't have a key yet, use GetPin to initiate pin
    ///     based authentication.
    /// </summary>
    /// <param name="agent">
    ///     You'll also need to identify your software or script by a meaningful agent parameter (your software user-agent).
    ///     Try to make it explicit, like the name of your software, script or library.
    /// </param>
    /// <param name="apiKey">
    ///     The Alldebrid API uses API keys to authenticate requests. You can view and manage your API keys in your Apikey dashboard,
    ///     (https://alldebrid.com/apikeys) or generate them remotely (with user action) through the PIN flow.
    /// </param>
    /// <param name="httpClient">
    ///     Optional HttpClient if you want to use your own HttpClient.
    /// </param>
    public AllDebridNETClient(String agent, String apiKey, HttpClient? httpClient = null)
    {
        if (String.IsNullOrWhiteSpace(agent))
        {
            throw new("Please provide an agent name, like the name of your app");
        }

        var client = httpClient ?? new HttpClient();

        _store.Agent = agent;
        _store.ApiKey = apiKey;

        Authentication = new AuthenticationApi(client, _store);
        Hosts = new HostsApi(client, _store);
        Links = new LinksApi(client, _store);
        Magnet = new MagnetApi(client, _store);
        System = new SystemApi(client, _store);
        User = new UserApi(client, _store);
        UserLink = new UserLinksApi(client, _store);
    }
}