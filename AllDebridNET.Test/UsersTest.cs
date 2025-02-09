using System.Threading.Tasks;
using Xunit;

namespace AllDebridNET.Test;

public class UsersTest
{
    [Fact]
    public async Task GetUser()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", "staticDemoApikeyPrem");

        var result = await client.User.GetAsync();

        Assert.NotNull(result);

        Assert.Equal("demoUserPremium", result.Username);
        Assert.Equal("demo@example.com", result.Email);
        Assert.True(result.IsPremium);
        Assert.False(result.IsTrial);
        Assert.False(result.IsSubscribed);
        Assert.True(result.PremiumUntil > 1740004110L);
        Assert.Equal("en", result.Lang);
        Assert.Equal("com", result.PreferedDomain);
        Assert.Equal(130, result.FidelityPoints);
        Assert.Equal(3, result.LimitedHostersQuotas!.Count);
        Assert.Empty(result.Notifications!);
    }

    [Fact]
    public async Task VerifyEmailStatusWaiting()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", "staticDemoApikeyPrem");

        var result = await client.User.VerifyEmailStatusAsync("verificationTokenWaiting3f9eb0db");

        Assert.Equal("waiting", result.VerifyEmailStatus);
        Assert.False(result.Resendable);
        Assert.Null(result.ApiKey);
    }
    
    [Fact]
    public async Task VerifyEmailStatusAllowed()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", "staticDemoApikeyPrem");

        var result = await client.User.VerifyEmailStatusAsync("verificationTokenAllowed3f9eb0db");

        Assert.Equal("allowed", result.VerifyEmailStatus);
        Assert.Null(result.Resendable);
        Assert.Equal("staticDemoApikeyPrem", result.ApiKey);
    }
    
    [Fact]
    public async Task VerifyEmailStatusDenied()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", "staticDemoApikeyPrem");

        var result = await client.User.VerifyEmailStatusAsync("verificationTokenDenied43f9eb0db");

        Assert.Equal("denied", result.VerifyEmailStatus);
        Assert.Null(result.Resendable);
        Assert.Null(result.ApiKey);
    }

    [Fact]
    public async Task ResendEmailNotification()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", "staticDemoApikeyPrem");

        var result = await client.User.ResendEmailNotificationAsync("verificationTokenResent43f9eb0db");

        Assert.True(result.Sent);
    }

    [Fact]
    public async Task ClearNotifications()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", "staticDemoApikeyPrem");

        var result = await client.User.ClearNotificationsAsync("NOTIF_CODE");

        Assert.Equal("Notification was cleared", result.Message);
    }
}