using System;
using System.Threading.Tasks;
using Xunit;

namespace AllDebridNET.Test;

public class MagnetTest
{
    [Fact]
    public async Task UploadMagnet()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", "staticDemoApikeyPrem");

        const String magnet = "magnet:?xt=urn:btih:3A842783e3005495d5d1637f5364b59343c7844707&dn=ubuntu-18.04.2-live-server-amd64.iso";

        var result = await client.Magnet.UploadMagnetAsync(magnet);

        Assert.NotNull(result);
        Assert.Equal("magnet:?xt=urn:btih:3A842783e3005495d5d1637f5364b59343c7844707&dn=ubuntu-18.04.2-live-server-amd64.iso", result.Magnet);
        Assert.Equal("3A842783e3005495d5d1637f5364b59343c7844707", result.Hash);
        Assert.Equal("ubuntu-18.04.2-live-server-amd64.iso", result.Name);
        Assert.Equal(48216647, result.Size);
        Assert.Equal(123456, result.Id);
        Assert.False(result.Ready);
    }

    [Fact]
    public async Task UploadFile()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", "staticDemoApikeyPrem");

        const String filePath = "ubuntu-18.04.2-live-server-amd64.iso.torrent";

        var file = await System.IO.File.ReadAllBytesAsync(filePath);

        var result = await client.Magnet.UploadFileAsync(file);

        Assert.NotNull(result);
        Assert.Null(result.Magnet);
        Assert.Equal("842783e3005495d5d1637f5364b59343c7844707", result.Hash);
        Assert.Equal("Ubuntu 18.04.2 live server amd64", result.Name);
        Assert.Equal(1954210119, result.Size);
        Assert.Equal(123456, result.Id);
        Assert.False(result.Ready);
    }
        
    [Fact]
    public async Task Status()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", "staticDemoApikeyPrem");

        var result = await client.Magnet.StatusAllAsync();

        Assert.Equal(3, result.Count);
    }

    [Fact]
    public async Task StatusActive()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", "staticDemoApikeyPrem");

        var result = await client.Magnet.StatusAllAsync("active");

        Assert.Single(result);
    }

    [Fact]
    public async Task Status_Single()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", "staticDemoApikeyPrem");

        var result = await client.Magnet.StatusAsync("56789");

        Assert.NotNull(result);

        Assert.Equal(56789, result.Id);
        Assert.Equal("ubuntu-20.04.2-live-server-amd64.iso", result.Filename);
        Assert.Equal("987654321cef825718eda30637230585e3330599", result.Hash);
        Assert.Equal(256400192L, result.Size);
        Assert.Equal("Ready", result.Status);
        Assert.Equal(4, result.StatusCode);
        Assert.Equal(1657133868, result.UploadDate);
        Assert.Equal(1657133968, result.CompletionDate);
    }

    [Fact]
    public async Task StatusLive()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", "staticDemoApikeyPrem");

        var result = await client.Magnet.StatusLiveAsync(1234, 0);

        Assert.NotNull(result.Magnets);
        Assert.Equal(3, result.Magnets.Count);
        Assert.True(result.Fullsync);
        Assert.Equal(1, result.Counter);
    }
    
    [Fact]
    public async Task StatusLiveDelta()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", "staticDemoApikeyPrem");

        var result = await client.Magnet.StatusLiveAsync(1234, 1);

        Assert.NotNull(result.Magnets);
        Assert.Equal(1, result.Magnets.Count);
        Assert.Null(result.Fullsync);
        Assert.Equal(2, result.Counter);
    }

    [Fact]
    public async Task Delete()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", "staticDemoApikeyPrem");

        var result = await client.Magnet.DeleteAsync("56789");

        Assert.Equal("Magnet was successfully deleted", result);
    }

    [Fact]
    public async Task RestartAsync()
    {
        var client = new AllDebridNETClient("AllDebridNETTest", "staticDemoApikeyPrem");

        var result = await client.Magnet.RestartAsync("99999");

        Assert.Equal("Magnet was successfully restarted", result);
    }
}