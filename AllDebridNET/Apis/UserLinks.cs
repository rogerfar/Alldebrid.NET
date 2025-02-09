using AllDebridNET.Models;

namespace AllDebridNET;

public class UserLinksApi
{
    private readonly Requests _requests;

    internal UserLinksApi(HttpClient httpClient, Store store)
    {
        _requests = new(httpClient, store);
    }

    /// <summary>
    ///     Use this endpoint to get links the user saved for later use.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<UserLink>> UserLinksAsync(CancellationToken cancellationToken = default)
    {
        var result = await _requests.GetRequestAsync<UserLinks>("user/links", true, null, cancellationToken);

        return result.Links;
    }

    /// <summary>
    ///    Use this endpoint to save links for later use.
    /// </summary>
    /// <param name="links">Links to save.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ActionResponse> SaveLinkAsync(List<String> links, CancellationToken cancellationToken = default)
    {
        var data = links.Select(link => new KeyValuePair<String, String>("links[]", link)).ToList();

        return await _requests.PostRequestAsync<ActionResponse>("user/links/save", data, true, cancellationToken);
    }

    /// <summary>
    ///    Use this endpoint to delete links the user saved for later use.
    /// </summary>
    /// <param name="links">Links to delete.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ActionResponse> DeleteLinksAsync(List<String> links, CancellationToken cancellationToken = default)
    {
        var data = links.Select(link => new KeyValuePair<String, String>("links[]", link)).ToList();

        return await _requests.PostRequestAsync<ActionResponse>($"user/links/delete", data, true, cancellationToken);
    }

    /// <summary>
    ///     Use this endpoint to get recent links. Recent link logging being disabled by default, this will return nothing until history logging has been activated in your account settings.
    ///     Links older than 3 days are automatically deleted from the recent history. To keep links in your account, use the Saved links.
    ///     You HAVE to enable history links loggging in your account settings before seeing any links being saved in this recent history. Recent link logging is disabled by default.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<UserLink>> GetHistoryAsync(CancellationToken cancellationToken = default)
    {
        var result = await _requests.GetRequestAsync<UserLinks>("user/history", true, null, cancellationToken);

        return result.Links;
    }

    /// <summary>
    ///     Use this endpoint to delete all links currently in your recent links history. Links older than 3 days are automatically deleted from the recent history.
    ///     You HAVE to enable history links loggging in your account settings before seeing any links being saved in this recent history. Recent link logging is disabled by default.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ActionResponse> DeleteHistoryAsync(CancellationToken cancellationToken = default)
    {
        return await _requests.PostRequestAsync<ActionResponse>($"user/history/delete", null, true, cancellationToken);
    }
}