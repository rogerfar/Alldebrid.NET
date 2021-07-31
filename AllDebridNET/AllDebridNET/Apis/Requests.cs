using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using RDNET.Exceptions;

namespace AllDebridNET.Apis
{
    internal class Requests
    {
        private readonly HttpClient _httpClient;
        private readonly Store _store;

        public Requests(HttpClient httpClient, Store store)
        {
            _httpClient = httpClient;
            _store = store;
        }

        private async Task<String> Request(String url,
                                           Boolean requireAuthentication, 
                                           RequestType requestType,
                                           HttpContent data,
                                           IDictionary<String, String> parameters,
                                           CancellationToken cancellationToken)
        {
            url = $"{_store.ApiUrl}{url}?agent={HttpUtility.UrlEncode(_store.Agent)}";

            if (parameters is {Count: > 0})
            {
                var parametersString = String.Join("&", parameters.Select(m => $"{m.Key}={HttpUtility.UrlEncode(m.Value)}"));

                url = $"{url}&{parametersString}";
            }

            _httpClient.DefaultRequestHeaders.Remove("Authorization");

            if (requireAuthentication)
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_store.ApiKey}");
            }

            var response = requestType switch
            {
                RequestType.Get => await _httpClient.GetAsync(url, cancellationToken),
                RequestType.Post => await _httpClient.PostAsync(url, data, cancellationToken),
                RequestType.Put => await _httpClient.PutAsync(url, data, cancellationToken),
                RequestType.Delete => await _httpClient.DeleteAsync(url, cancellationToken),
                _ => throw new ArgumentOutOfRangeException(nameof(requestType), requestType, null)
            };

            var buffer = await response.Content.ReadAsByteArrayAsync();
            var text = Encoding.UTF8.GetString(buffer, 0, buffer.Length);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                text = null;
            }
            
            return text;
        }
        
        private async Task<T> Request<T>(String url,
                                         Boolean requireAuthentication,
                                         RequestType requestType,
                                         HttpContent data,
                                         IDictionary<String, String> parameters,
                                         CancellationToken cancellationToken)
            where T : class, new()
        {
            var requestResult = await Request(url, requireAuthentication, requestType, data, parameters, cancellationToken);

            if (requestResult == null)
            {
                return new T();
            }

            try
            {
                var result = JsonConvert.DeserializeObject<Response<T>>(requestResult);

                if (result == null)
                {
                    throw new Exception("Response was null");
                }

                if (result.Status != "success")
                {
                    if (result.Error != null && !String.IsNullOrWhiteSpace(result.Error.Message))
                    {
                        throw new AllDebridException(result.Error.Message, result.Error.Code);
                    }

                    throw new JsonSerializationException($"Unknown error. Response was: {result}");
                }

                return result.Data;
            }
            catch (JsonSerializationException ex)
            {
                throw new JsonSerializationException($"Unable to deserialize AllDebrid API response to {typeof(T).Name}. Response was: {requestResult}", ex);
            }
        }

        public async Task<T> GetRequestAsync<T>(String url, Boolean requireAuthentication, IDictionary<String, String> parameters, CancellationToken cancellationToken)
            where T : class, new()
        {
            return await Request<T>(url, requireAuthentication, RequestType.Get, null, parameters, cancellationToken);
        }

        public async Task PostRequestAsync(String url, IEnumerable<KeyValuePair<String, String>> data, Boolean requireAuthentication, CancellationToken cancellationToken)
        {
            var content = data != null ? new FormUrlEncodedContent(data) : null;
            await Request(url, requireAuthentication, RequestType.Post, content, null, cancellationToken);
        }

        public async Task<T> PostRequestAsync<T>(String url, IEnumerable<KeyValuePair<String, String>> data, Boolean requireAuthentication, CancellationToken cancellationToken)
            where T : class, new()
        {
            var content = data != null ? new FormUrlEncodedContent(data) : null;
            return await Request<T>(url, requireAuthentication, RequestType.Post, content, null, cancellationToken);
        }

        public async Task PutRequestAsync(String url, Byte[] file, Boolean requireAuthentication, CancellationToken cancellationToken)
        {
            var content = new ByteArrayContent(file);
            await Request(url, requireAuthentication, RequestType.Put, content, null, cancellationToken);
        }

        public async Task<T> PutRequestAsync<T>(String url, Byte[] file, Boolean requireAuthentication, CancellationToken cancellationToken)
            where T : class, new()
        {
            var content = new ByteArrayContent(file);
            return await Request<T>(url, requireAuthentication, RequestType.Put, content, null, cancellationToken);
        }

        public async Task DeleteRequestAsync(String url, Boolean requireAuthentication, CancellationToken cancellationToken)
        {
            await Request(url, requireAuthentication, RequestType.Delete, null, null, cancellationToken);
        }

        private enum RequestType
        {
            Get,
            Post,
            Put,
            Delete
        }
    }
}
