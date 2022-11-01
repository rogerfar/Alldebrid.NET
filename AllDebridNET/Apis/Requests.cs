using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using AllDebridNET.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RDNET;

namespace AllDebridNET.Apis;

internal class Requests
{
    private readonly HttpClient _httpClient;
    private readonly Store _store;

    public static readonly JsonSerializerSettings JsonSerializerSettings = new()
    {
        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
        DateParseHandling = DateParseHandling.None,
        Converters =
        {
            new FileEUnionConverter(),
            new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
        },
    };

    public Requests(HttpClient httpClient, Store store)
    {
        _httpClient = httpClient;
        _store = store;
    }

    private async Task<String?> Request(String url,
                                        Boolean requireAuthentication, 
                                        RequestType requestType,
                                        HttpContent? data,
                                        IDictionary<String, String>? parameters,
                                        CancellationToken cancellationToken)
    {
        url = $"{Store.API_URL}{url}?agent={HttpUtility.UrlEncode(_store.Agent)}";

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
                                     HttpContent? data,
                                     IDictionary<String, String>? parameters,
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
            var result = JsonConvert.DeserializeObject<Response<T>>(requestResult, JsonSerializerSettings);

            if (result == null)
            {
                throw new Exception("Response was null");
            }

            if (result.Status != "success")
            {
                if (result.Error != null && !String.IsNullOrWhiteSpace(result.Error.Message))
                {
                    throw new AllDebridException(result.Error.Message!, result.Error.Code!);
                }

                throw new JsonSerializationException($"Unknown error. Response was: {result}");
            }

            if (result.Data == null)
            {
                return new T();
            }

            return result.Data;
        }
        catch (Exception ex)
        {
            throw new Exception($"Unable to deserialize AllDebrid API response to {typeof(T).Name}. Response was: {requestResult}", ex);
        }
    }
        
    public async Task<T> GetRequestAsync<T>(String url, Boolean requireAuthentication, IDictionary<String, String>? parameters, CancellationToken cancellationToken)
        where T : class, new()
    {
        return await Request<T>(url, requireAuthentication, RequestType.Get, null, parameters, cancellationToken);
    }
        
    public async Task<T> PostRequestAsync<T>(String url, IEnumerable<KeyValuePair<String, String>>? data, Boolean requireAuthentication, CancellationToken cancellationToken)
        where T : class, new()
    {
        var content = data != null ? new FormUrlEncodedContent(data) : null;
        return await Request<T>(url, requireAuthentication, RequestType.Post, content, null, cancellationToken);
    }

    public async Task<T> PostFileRequestAsync<T>(String url, Byte[] file, Boolean requireAuthentication, CancellationToken cancellationToken)
        where T : class, new()
    {
        using var multipartFormDataContent = new MultipartFormDataContent();
        multipartFormDataContent.Headers.ContentType.MediaType = "multipart/form-data";

        var fileContent = new StreamContent(new MemoryStream(file));
        fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data") 
        { 
            Name = "files[]",
            FileName = "1.torrent"
        };
        fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-bittorrent");

        multipartFormDataContent.Add(fileContent);
            
        return await Request<T>(url, requireAuthentication, RequestType.Post, multipartFormDataContent, null, cancellationToken);
    }
        
    private enum RequestType
    {
        Get,
        Post,
        Put,
        Delete
    }
}