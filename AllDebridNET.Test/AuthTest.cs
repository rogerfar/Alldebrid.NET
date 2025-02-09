using System.Threading.Tasks;
using Xunit;

namespace AllDebridNET.Test;

public class AuthTest
{
    [Fact]
    public async Task GetPin()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", "staticDemoApikeyPrem");

        var result = await client.Authentication.GetPinAsync();

        Assert.Equal("123A", result.Pin);
        Assert.Equal("664c3ca2635c99f291d28e11ea18e154750bd21a", result.Check);
        Assert.Equal("https://alldebrid.com/pin/?pin=123A", result.UserUrl);
        Assert.Equal(600, result.ExpiresIn);
        Assert.Equal("https://alldebrid.com/pin/", result.BaseUrl);
    }

    [Fact]
    public async Task CheckPinWaiting()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", "staticDemoApikeyPrem");

        var result = await client.Authentication.CheckPinAsync("664c3ca2635c99f291d28e11ea18e154750bd21a", "123A");

        Assert.False(result.Activated);
        Assert.Equal(423, result.ExpiresIn);
        Assert.Null(result.Apikey);
    }
    
    [Fact]
    public async Task CheckPinSuccess()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", "staticDemoApikeyPrem");

        var result = await client.Authentication.CheckPinAsync("664c3ca2635c99f291d28e11ea18e154750bd21a", "123B");

        Assert.True(result.Activated);
        Assert.Equal(123, result.ExpiresIn);
        Assert.Equal("staticDemoApikeyPrem", result.Apikey);
    }
}