using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;

namespace AllDebridNET;

internal class Requests(HttpClient httpClient, Store store)
{
    private static JsonSerializerOptions JsonSerializerSettings => new()
    {
        PropertyNameCaseInsensitive = true,
        NumberHandling = JsonNumberHandling.AllowReadingFromString
    };

    private async Task<String?> Request(String url,
                                        Boolean requireAuthentication, 
                                        RequestType requestType,
                                        HttpContent? data,
                                        IDictionary<String, String>? parameters,
                                        CancellationToken cancellationToken)
    {
        url = $"{Store.API_URL}{Store.API_VERSION}{url}?agent={HttpUtility.UrlEncode(store.Agent)}";
        
        if (parameters is {Count: > 0})
        {
            var parametersString = String.Join("&", parameters.Select(m => $"{m.Key}={HttpUtility.UrlEncode(m.Value)}"));

            url = $"{url}&{parametersString}";
        }

        httpClient.DefaultRequestHeaders.Remove("Authorization");

        if (requireAuthentication)
        {
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {store.ApiKey}");
        }

        var response = requestType switch
        {
            RequestType.Get => await httpClient.GetAsync(url, cancellationToken),
            RequestType.Post => await httpClient.PostAsync(url, data, cancellationToken),
            RequestType.Put => await httpClient.PutAsync(url, data, cancellationToken),
            RequestType.Delete => await httpClient.DeleteAsync(url, cancellationToken),
            _ => throw new ArgumentOutOfRangeException(nameof(requestType), requestType, null)
        };

        var buffer = await response.Content.ReadAsByteArrayAsync(cancellationToken);
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
            return new();
        }

        try
        {
            var result = JsonSerializer.Deserialize<Response<T>>(requestResult, JsonSerializerSettings) ?? throw new("Response was null");

            if (result.Status != "success")
            {
                if (result.Error != null && !String.IsNullOrWhiteSpace(result.Error.Message))
                {
                    throw new AllDebridException(result.Error.Message!, result.Error.Code!);
                }

                throw new($"Unknown error. Response was: {result}");
            }

            if (result.Data == null)
            {
                return new();
            }

            return result.Data;
        }
        catch (AllDebridException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new($"Unable to deserialize AllDebrid API response to {typeof(T).Name}. Response was: {requestResult}", ex);
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
        multipartFormDataContent.Headers.ContentType!.MediaType = "multipart/form-data";

        var fileContent = new StreamContent(new MemoryStream(file));
        fileContent.Headers.ContentDisposition = new("form-data") 
        { 
            Name = "files[]",
            FileName = "1.torrent"
        };
        fileContent.Headers.ContentType = new("application/x-bittorrent");

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