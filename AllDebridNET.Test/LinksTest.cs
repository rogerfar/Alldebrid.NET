using System.Threading.Tasks;
using Xunit;

namespace AllDebridNET.Test;

public class LinksTest
{
    [Fact]
    public async Task Informations_Success()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", "staticDemoApikeyPrem");

        var results = await client.Links.InformationsAsync(["http://example.com/somefile"], "optionalPassword");

        Assert.Single(results);

        Assert.Equal("http://example.com/somefile", results[0].Link);
        Assert.Equal("example.somefile.txt", results[0].Filename);
        Assert.Equal(10240L, results[0].Size);
        Assert.Equal("example", results[0].Host);
        Assert.Equal("example.com", results[0].HostDomain);
        Assert.Null(results[0].Error);
    }

    [Fact]
    public async Task Informations_Errors()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", "staticDemoApikeyPrem");

        var results = await client.Links.InformationsAsync(["https://example.com/down", "https://not-supported.com/somefile.txt"], "optionalPassword");

        Assert.Equal(2, results.Count);

        Assert.Equal("https://example.com/down", results[0].Link);
        Assert.Equal("LINK_DOWN", results[0].Error!.Code);
        Assert.Equal("This link is not available on the file hoster website", results[0].Error!.Message);

        Assert.Equal("https://not-supported.com/somefile.txt", results[1].Link);
        Assert.Equal("LINK_HOST_NOT_SUPPORTED", results[1].Error!.Code);
        Assert.Equal("This host or link is not supported", results[1].Error!.Message);
    }

    [Fact]
    public async Task Redirector()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", "staticDemoApikeyPrem");

        var results = await client.Links.RedirectorAsync("https://example.com/redir/folder");

        Assert.Equal(2, results.Count);

        Assert.Equal("https://redirect.alldebrid.com/mnsaj-0-fcdeaf3287", results[0]);
        Assert.Equal("https://redirect.alldebrid.com/mnsaj-1-fcdeaf3287", results[1]);
    }
        
    [Fact]
    public async Task DownloadLink()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", "staticDemoApikeyPrem");

        var result = await client.Links.DownloadLinkAsync("https://example.com/file2MB.txt");

        Assert.Equal("https://ombfyx.debrid.it/dl/abcdefgh12/example.file2MB.txt.txt", result.Link);
        Assert.Equal("example", result.Host);
        Assert.Equal("example.file2MB.txt.txt", result.Filename);
        Assert.Equal(2097152L, result.Filesize);
        Assert.Equal("3hk19hz3205", result.Id);
        Assert.Equal("example.com", result.HostDomain);
    }

    [Fact]
    public async Task StreamLink()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", "staticDemoApikeyPrem");

        var result = await client.Links.StreamingLinkAsync("demov1bc797", "hd-720");

        Assert.Equal("http://ombfyx.alld.io/dl/abcdefgh12/somefile.mp4", result.Link);
        Assert.Equal("Some video", result.Filename);
        Assert.Equal(699400192L, result.Filesize);
    }

    [Fact]
    public async Task DelayedLinkWaiting()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", "staticDemoApikeyPrem");

        var result = await client.Links.DelayedAsync("123456");

        Assert.Equal(1, result.Status);
        Assert.Equal(30, result.TimeLeft);
        Assert.Null(result.Link);
    }
    
    [Fact]
    public async Task DelayedLinkReady()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", "staticDemoApikeyPrem");

        var result = await client.Links.DelayedAsync("1234567");

        Assert.Equal(2, result.Status);
        Assert.Equal(0, result.TimeLeft);
        Assert.Equal("https://ombfyx.debrid.it/dl/abcdefgh12/file.txt", result.Link);
    }
}