using System.Threading.Tasks;
using Xunit;

namespace AllDebridNET.Test;

public class HostsTest
{
    [Fact]
    public async Task Hosts()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", Setup.ApiKey);

        var result = await client.Hosts.GetHostsAsync();

        Assert.True(result.HostList.Count > 50);
    }

    [Fact]
    public async Task Domains()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", Setup.ApiKey);

        var result = await client.Hosts.GetDomainsAsync();

        Assert.True(result.Hosts.Count > 50);
    }
        
    [Fact]
    public async Task HostPriority()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", Setup.ApiKey);

        var result = await client.Hosts.GetHostsPriorityAsync();

        Assert.True(result.Count > 50);
    }
        
    [Fact]
    public async Task HostsForUser()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", Setup.ApiKey);

        var result = await client.Hosts.GetHostsForUserAsync();

        Assert.True(result.HostList.Count > 50);
    }
}