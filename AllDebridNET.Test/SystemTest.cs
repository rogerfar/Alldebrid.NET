using System.Threading.Tasks;
using Xunit;

namespace AllDebridNET.Test;

public class SystemTest
{
    [Fact]
    public async Task Ping()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", "staticDemoApikeyPrem");

        var result = await client.System.PingAsync();

        Assert.Equal("pong", result.Ping);
        Assert.Equal("4.1", result.Version);
        Assert.True(result.Subversion);
    }
}