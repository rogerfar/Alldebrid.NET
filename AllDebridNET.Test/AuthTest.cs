﻿using System.Threading.Tasks;
using Xunit;

namespace AllDebridNET.Test;

public class AuthTest
{
    [Fact]
    public async Task GetPin()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", Setup.ApiKey);

        var result = await client.Authentication.GetPinAsync();

        Assert.NotNull(result.Check);
    }

    [Fact]
    public async Task CheckPin()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", Setup.ApiKey);

        var result = await client.Authentication.CheckPinAsync("9b6809b96196363b56f3afc68a2ddb32537305c5", "BPW4");

        Assert.NotNull(result.Apikey);
    }
}