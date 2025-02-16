namespace AllDebridNET;

public interface ILinksApi
{
    /// <summary>
    ///     Use this endpoint to retrieve informations about a link. If it is in our systems, you'll have the filename and size
    ///     (if available).
    ///     If the host is not supported or the link is down, an error will be returned for that link.
    ///     This endpoint only support host links, not redirectors links. Use the link/redirector endpoint for this.
    /// </summary>
    /// <param name="links">The list of links you request informations about.</param>
    /// <param name="password">Link password (supported on uptobox / 1fichier).</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<LinkInfo>> InformationsAsync(List<String> links, String? password = null, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Use this endpoint to retrieve links protected by a redirector or link protector.
    /// </summary>
    /// <param name="link">The redirector or protector link to extract links.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<String>> RedirectorAsync(String link, CancellationToken cancellationToken = default);

    /// <summary>
    ///     This endpoint unlocks a given link.
    ///     This endpoint can return a delayed ID. In that case, you must follow the delayed link flow.
    /// </summary>
    /// <param name="link">The link to unlock.</param>
    /// <param name="password">Link password (supported on uptobox / 1fichier).</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<DownloadLink> DownloadLinkAsync(String link, String? password = null, CancellationToken cancellationToken = default);

    /// <summary>
    ///     The unlocking flow for streaming link is a bit more complex.
    ///     First hit the usual link/unlock endpoint. Two cases :
    ///     Stream link has only one quality : downloading link is available immediatly.
    ///     OR
    ///     Stream links has multiple qualities : you must select the desired quality to obtain a download link or delayed id
    ///     by using the link/streaming endpoint.
    ///     Depending of the stream website, you'll either get a download link, or a delayed id (see Delayed link section for
    ///     delayed links).
    /// </summary>
    /// <param name="id">The link ID you received from the /link/unlock call.</param>
    /// <param name="streamId">The stream ID you choosed from the stream qualities list returned by /link/unlock.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<StreamingLink> StreamingLinkAsync(String id, String streamId, CancellationToken cancellationToken = default);

    /// <summary>
    ///     This endpoint give the status of a delayed link.
    ///     Some links need time to generate, this endpoint send the status of such delayed links.
    ///     You should pool every 5 seconds or more the link/delayed endpoint until given the download link.
    /// </summary>
    /// <param name="delayedId">Delayed ID received in /link/unlock.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<DelayedLink> DelayedAsync(String delayedId, CancellationToken cancellationToken = default);
}

public class LinksApi : ILinksApi
{
    private readonly Requests _requests;

    internal LinksApi(HttpClient httpClient, Store store)
    {
        _requests = new(httpClient, store);
    }

    /// <inheritdoc />
    public async Task<List<LinkInfo>> InformationsAsync(List<String> links, String? password = null, CancellationToken cancellationToken = default)
    {
        var parameters = new Dictionary<String, String>();

        for (var i = 0; i < links.Count; i++)
        {
            parameters.Add($"link[{i}]", links[i]);
        }

        if (!String.IsNullOrWhiteSpace(password))
        {
            parameters.Add("password", password);
        }

        var result = await _requests.GetRequestAsync<LinksInformationsResponse>("link/infos", true, parameters, cancellationToken);

        return result.Infos ?? [];
    }

    /// <inheritdoc />
    public async Task<List<String>> RedirectorAsync(String link, CancellationToken cancellationToken = default)
    {
        var parameters = new Dictionary<String, String>
        {
            {
                "link", link
            }
        };

        var result = await _requests.GetRequestAsync<LinksRedirectorResponse>("link/redirector", true, parameters, cancellationToken);

        return result.Links ?? [];
    }

    /// <inheritdoc />
    public async Task<DownloadLink> DownloadLinkAsync(String link, String? password = null, CancellationToken cancellationToken = default)
    {
        var parameters = new Dictionary<String, String>
        {
            {
                "link", link
            }
        };

        if (!String.IsNullOrWhiteSpace(password))
        {
            parameters.Add("password", password!);
        }

        return await _requests.GetRequestAsync<DownloadLink>("link/unlock", true, parameters, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<StreamingLink> StreamingLinkAsync(String id, String streamId, CancellationToken cancellationToken = default)
    {
        var parameters = new Dictionary<String, String>
        {
            {
                "id", id
            },
            {
                "stream", streamId
            }
        };

        return await _requests.GetRequestAsync<StreamingLink>("link/streaming", true, parameters, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<DelayedLink> DelayedAsync(String delayedId, CancellationToken cancellationToken = default)
    {
        var parameters = new Dictionary<String, String>
        {
            {
                "id", delayedId
            },
        };

        return await _requests.GetRequestAsync<DelayedLink>("link/delayed", true, parameters, cancellationToken);
    }
}