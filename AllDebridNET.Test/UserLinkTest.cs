using System.Threading.Tasks;
using Xunit;

namespace AllDebridNET.Test;

public class UserLinksTest
{
    [Fact]
    public async Task UserLinks()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", "staticDemoApikeyPrem");

        var results = await client.UserLink.UserLinksAsync();

        Assert.Equal(2, results.Count);

        Assert.Equal("https://example.com/l1ub83vil5c3", results[0].Link);
        Assert.Equal("ubuntu.16.04.1.iso", results[0].Filename);
        Assert.Equal(671088640L, results[0].Size);
        Assert.Equal(1546431734L, results[0].Date);
        Assert.Equal("example", results[0].Host);
        
        Assert.Equal("https://example.com/aaaabbbbcccc", results[1].Link);
        Assert.Equal("winter.holidays.photos.zip", results[1].Filename);
        Assert.Equal(91088640L, results[1].Size);
        Assert.Equal(1546421734L, results[1].Date);
        Assert.Equal("example", results[1].Host);
    }

    [Fact]
    public async Task SaveLink()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", "staticDemoApikeyPrem");
        var results = await client.UserLink.SaveLinkAsync([
            "https://example.com/file2MB.txt",
            "https://example.com/file10MB.txt"
        ]);
        Assert.Equal("Links were sucessfully saved", results.Message);
    }

    [Fact]
    public async Task DeleteLinks()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", "staticDemoApikeyPrem");
        var results = await client.UserLink.DeleteLinksAsync([
            "https://example.com/file2MB.txt"
        ]);
        Assert.Equal("Links were sucessfully deleted", results.Message);
    }

    [Fact]
    public async Task GetHistory()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", "staticDemoApikeyPrem");
        var results = await client.UserLink.GetHistoryAsync();

        Assert.Equal(2, results.Count);
    }

    [Fact]
    public async Task DeleteHistory()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", "staticDemoApikeyPrem");
        var results = await client.UserLink.DeleteHistoryAsync();

        Assert.Equal("Recent links history was successfully purged", results.Message);
    }
}