using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AllDebridNET.Apis
{
    public class MagnetApi
    {
        private readonly Requests _requests;

        internal MagnetApi(HttpClient httpClient, Store store)
        {
            _requests = new Requests(httpClient, store);
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
        public async Task<MagnetAddResult> UploadMagnetAsync(String magnetLink, CancellationToken cancellationToken = default)
        {
            var data = new[]
            {
                new KeyValuePair<String, String>("magnets", magnetLink)
            };

            var result = await _requests.PostRequestAsync<MagnetUploadResponse>("magnet/upload", data, true, cancellationToken);

            var magnetResult = result?.Magnets?.FirstOrDefault();

            if (magnetResult == null)
            {
                return null;
            }

            if (magnetResult.Id == null || magnetResult.Id == 0)
            {
                var retry = 0;
                while (true)
                {
                    var allStatus = await StatusAllAsync(cancellationToken);
                    var statusResult = allStatus.FirstOrDefault(m => m.Hash == magnetResult.Hash);

                    if (statusResult != null)
                    {
                        magnetResult.Id = statusResult.Id;
                        return magnetResult;
                    }

                    if (retry == 10)
                    {
                        throw new Exception("Unable to find ID for magnet");
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
        public async Task<MagnetAddResult> UploadFileAsync(Byte[] file, CancellationToken cancellationToken = default)
        {
            var result = await _requests.PostFileRequestAsync<MagnetUploadResponse>("magnet/upload/file", file, true, cancellationToken);

            var fileResult = result?.Files?.FirstOrDefault();

            if (fileResult == null)
            {
                return null;
            }

            if (fileResult.Id == null || fileResult.Id == 0)
            {
                var retry = 0;
                while (true)
                {
                    var allStatus = await StatusAllAsync(cancellationToken);
                    var statusResult = allStatus.FirstOrDefault(m => m.Hash == fileResult.Hash);

                    if (statusResult != null)
                    {
                        fileResult.Id = statusResult.Id;
                        return fileResult;
                    }

                    if (retry == 10)
                    {
                        throw new Exception("Unable to find ID for magnet");
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
        /// <param name="cancellationToken"></param>
        /// <returns>
        ///     List of Magnet
        /// </returns>
        public async Task<IList<Magnet>> StatusAllAsync(CancellationToken cancellationToken = default)
        {
            var result = await _requests.GetRequestAsync<MagnetsStatusResponse>("magnet/status", true, null, cancellationToken);

            return result.Magnets;
        }

        /// <summary>
        ///     Get the status of current magnets.
        /// </summary>
        /// <param name="magnetId">
        ///     Magnet ID.
        /// </param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        ///     List of Magnet
        /// </returns>
        public async Task<Magnet> StatusAsync(String magnetId, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<String, String>
            {
                {
                    "id", magnetId
                }
            };

            var result = await _requests.GetRequestAsync<MagnetStatusResponse>("magnet/status", true, parameters, cancellationToken);

            return result.Magnets;
        }

        /// <summary>
        ///     Delete a magnet.
        /// </summary>
        /// <param name="magnetId">Magnet ID.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task DeleteAsync(String magnetId, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<String, String>
            {
                {
                    "id", magnetId
                }
            };

            await _requests.GetRequestAsync<MagnetStatusResponse>("magnet/delete", true, parameters, cancellationToken);
        }

        /// <summary>
        ///     Restart a failed magnet.
        /// </summary>
        /// <param name="magnetId">Magnet ID.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task RestartAsync(String magnetId, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<String, String>
            {
                {
                    "id", magnetId
                }
            };

            await _requests.GetRequestAsync<MagnetStatusResponse>("magnet/restart", true, parameters, cancellationToken);
        }

        /// <summary>
        ///     Check if a magnet is available instantly.
        /// </summary>
        /// <param name="magnet">Magnets URI or hash you wish to check instant availability.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Boolean> InstantAvailabilityAsync(String magnet, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<String, String>
            {
                {
                    "magnets[]", magnet
                }
            };

            var result = await _requests.GetRequestAsync<InstantAvailabilityResponse>("magnet/instant", true, parameters, cancellationToken);

            return result.Magnets.Any(m => m.Instant);
        }
    }
}
