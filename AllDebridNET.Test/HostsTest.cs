using System.Threading.Tasks;
using Xunit;

namespace AllDebridNET.Test;

public class HostsTest
{
    [Fact]
    public async Task Hosts()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", "staticDemoApikeyPrem");

        var result = await client.Hosts.GetHostsAsync();

        Assert.NotNull(result.HostList);
        Assert.NotNull(result.Redirectors);
        Assert.NotNull(result.Streams);
        Assert.NotEmpty(result.HostList);
        Assert.NotEmpty(result.Redirectors);
        Assert.NotEmpty(result.Streams);
    }

    [Fact]
    public async Task Domains()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", "staticDemoApikeyPrem");

        var result = await client.Hosts.GetDomainsAsync();

        Assert.NotNull(result.Hosts);
        Assert.NotNull(result.Redirectors);
        Assert.NotNull(result.Streams);
        Assert.NotEmpty(result.Hosts);
        Assert.NotEmpty(result.Redirectors);
        Assert.NotEmpty(result.Streams);
    }
        
    [Fact]
    public async Task HostPriority()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", "staticDemoApikeyPrem");

        var result = await client.Hosts.GetHostsPriorityAsync();

        Assert.NotEmpty(result);
    }
        
    [Fact]
    public async Task HostsForUser()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", "staticDemoApikeyPrem");

        var result = await client.Hosts.GetHostsForUserAsync();

        Assert.NotNull(result.HostList);
        Assert.Null(result.Redirectors);
        Assert.Null(result.Streams);
        Assert.NotEmpty(result.HostList);
    }
}