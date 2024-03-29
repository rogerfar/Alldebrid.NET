﻿using System.Threading.Tasks;
using Xunit;

namespace AllDebridNET.Test;

public class UsersTest
{
    [Fact]
    public async Task GetUser()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", Setup.ApiKey);

        var result = await client.User.GetAsync();

        Assert.Contains(result.Email, "@");
    }

    [Fact]
    public async Task ClearNotifications()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", Setup.ApiKey);

        var result = await client.User.ClearNotificationsAsync("NOTIF_CODE");

        Assert.Equal("Notification was cleared", result.Message);
    }
}