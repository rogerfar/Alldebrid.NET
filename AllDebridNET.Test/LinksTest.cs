using System.Threading.Tasks;
using Xunit;

namespace AllDebridNET.Test;

public class LinksTest
{
    [Fact]
    public async Task Informations()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", Setup.ApiKey);

        var result = await client.Links.InformationsAsync("http://uptobox.com/l1ub83vil5c3");
    }

    [Fact]
    public async Task Redirector()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", Setup.ApiKey);

        var result = await client.Links.RedirectorAsync("http://uptobox.com/l1ub83vil5c3");
    }
        
    [Fact]
    public async Task DownloadLink()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", Setup.ApiKey);

        var result = await client.Links.DownloadLinkAsync("https://uptobox.com/q68hv04slqvg");
    }

    [Fact]
    public async Task StreamLink()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", Setup.ApiKey);

        var result = await client.Links.StreamingLinkAsync("28swlyd5e54", "360");
    }
}