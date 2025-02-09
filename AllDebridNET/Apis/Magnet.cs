using AllDebridNET.Models;

namespace AllDebridNET;

public class MagnetApi
{
    private readonly Requests _requests;

    internal MagnetApi(HttpClient httpClient, Store store)
    {
        _requests = new(httpClient, store);
    }

    /// <summary>
    ///     Upload a magnet with its URI or hash.
    /// </summary>
    /// <param name="magnetLink">
    ///     The magnet link or URI.
    /// </param>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    public async Task<MagnetAddResult?> UploadMagnetAsync(String magnetLink, CancellationToken cancellationToken = default)
    {
        var data = new[]
        {
            new KeyValuePair<String, String>("magnets", magnetLink)
        };

        var result = await _requests.PostRequestAsync<MagnetUploadResponse>("magnet/upload", data, true, cancellationToken);

        var magnetResult = result.Magnets?.FirstOrDefault();

        if (magnetResult == null)
        {
            return null;
        }

        if (magnetResult.Id == null || magnetResult.Id == 0)
        {
            var retry = 0;
            while (true)
            {
                var allStatus = await StatusAllAsync(null, cancellationToken);
                var statusResult = allStatus.FirstOrDefault(m => m.Hash == magnetResult.Hash);

                if (statusResult != null)
                {
                    magnetResult.Id = statusResult.Id;
                    return magnetResult;
                }

                if (retry == 10)
                {
                    throw new("Unable to find ID for magnet");
                }

                retry++;

                await Task.Delay(1000, cancellationToken);
            }
        }

        return magnetResult;
    }

    /// <summary>
    ///     Upload torrent files.
    /// </summary>
    /// <param name="file">
    ///     The file as a byte array.
    /// </param>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    public async Task<MagnetAddResult?> UploadFileAsync(Byte[] file, CancellationToken cancellationToken = default)
    {
        var result = await _requests.PostFileRequestAsync<MagnetUploadResponse>("magnet/upload/file", file, true, cancellationToken);

        var fileResult = result.Files?.FirstOrDefault();

        if (fileResult == null)
        {
            return null;
        }

        if (fileResult.Id == null || fileResult.Id == 0)
        {
            var retry = 0;
            while (true)
            {
                var allStatus = await StatusAllAsync(null, cancellationToken);
                var statusResult = allStatus.FirstOrDefault(m => m.Hash == fileResult.Hash);

                if (statusResult != null)
                {
                    fileResult.Id = statusResult.Id;
                    return fileResult;
                }

                if (retry == 10)
                {
                    throw new("Unable to find ID for magnet");
                }

                retry++;

                await Task.Delay(1000, cancellationToken);
            }
        }

        return fileResult;
    }

    /// <summary>
    ///     Get the status of current magnets.
    /// </summary>
    /// <param name="status">Magnets status filter. Either active, ready, expired or error</param>
    /// <param name="cancellationToken"></param>
    /// <returns>
    ///     List of Magnet
    /// </returns>
    public async Task<List<Magnet>> StatusAllAsync(String? status = null, CancellationToken cancellationToken = default)
    {
        var parameters = new Dictionary<String, String>();

        if (status != null)
        {
            parameters.Add("status", status);
        }

        var result = await _requests.GetRequestAsync<MagnetsStatusResponse>("magnet/status", true, parameters, cancellationToken);

        return result.Magnets ?? [];
    }

    /// <summary>
    ///     Get the status of current magnets.
    /// </summary>
    /// <param name="magnetId">
    ///     Magnet ID.
    /// </param>
    /// <param name="cancellationToken"></param>
    /// <returns>
    ///     The Magnet or null.
    /// </returns>
    public async Task<Magnet?> StatusAsync(String magnetId, CancellationToken cancellationToken = default)
    {
        var parameters = new Dictionary<String, String>
        {
            {
                "id", magnetId
            }
        };

        var result = await _requests.GetRequestAsync<MagnetStatusResponse>("magnet/status", true, parameters, cancellationToken);

        return result.Magnets?.FirstOrDefault();
    }

    /// <summary>
    ///    The Live Mode allows to only get the new data of the status of current magnets. It is designed to make a "live" panel or monitoring system more performant when consuming the magnet/status endpoint very frequently.
    ///    It requires a session ID and a counter, and using cache on the API side only the differences between the last state and the current state are sent, greatly reducing the amount of data returned by the API on each call.
    ///    The client using this mode must keep the current state of the magnets status locally between each call in order to apply the new data on the last state to get the whole current state.
    ///    A fixed session ID (integer) must be randomly set, and a counter starting at 0 will be used. On the first call (id=123, counter=0) with a new session ID, all the current data will be sent back, with the fullsync property set to true to make it clear, and the next counter to use. On the next call the updated counter is used (id=123, counter=1), and only the differences with the previous state will be send back.
    ///    If the magnets property returned is empty, then no change happened since the last call. If some changes happened, the magnets array will have some magnet objects (see Status) with its id and the properties changed, like this :
    ///    { "id": 123456, "downloaded": 258879224, "downloadSpeed": 20587738 }
    ///    You can them apply those diff to the last state you kept to get the current magnets status state.
    ///    If you send a counter that is not in sync with the last call response (like sending the same counter twice in a row), then the endpoint will consider your counter invalid and will return a full fullsync reponse with a reseted counter.
    ///    If you want to see a live implementation of this mode, it is currently in use on the magnet dashboard on Alldebrid.
    /// </summary>
    /// <param name="session">
    ///     Session ID.
    /// </param>
    /// <param name="counter">
    ///     Counter.
    /// </param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<MagnetStatusLiveResponse> StatusLiveAsync(Int64 session, Int64 counter, CancellationToken cancellationToken = default)
    {
        var data = new[]
        {
            new KeyValuePair<String, String>("session", session.ToString()),
            new KeyValuePair<String, String>("counter", counter.ToString())
        };

        return await _requests.PostRequestAsync<MagnetStatusLiveResponse>("magnet/status", data, true, cancellationToken);
    }

    /// <summary>
    ///     Delete a magnet.
    /// </summary>
    /// <param name="magnetId">Magnet ID.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<String?> DeleteAsync(String magnetId, CancellationToken cancellationToken = default)
    {
        var parameters = new Dictionary<String, String>
        {
            {
                "id", magnetId
            }
        };

        var result = await _requests.GetRequestAsync<ActionResponse>("magnet/delete", true, parameters, cancellationToken);

        return result.Message;
    }

    /// <summary>
    ///     Restart a failed magnet.
    /// </summary>
    /// <param name="magnetId">Magnet ID.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<String?> RestartAsync(String magnetId, CancellationToken cancellationToken = default)
    {
        var parameters = new Dictionary<String, String>
        {
            {
                "id", magnetId
            }
        };

        var result = await _requests.GetRequestAsync<ActionResponse>("magnet/restart", true, parameters, cancellationToken);

        return result.Message;
    }
}