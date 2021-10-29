using System;
using System.Threading.Tasks;
using RDNET.Test;
using Xunit;

namespace AllDebridNET.Test
{
    public class MagnetTest
    {
        [Fact]
        public async Task UploadMagnet()
        {
            var client = new AllDebridNETClient("AllDebridNETTest", Setup.ApiKey);

            const String magnet = "magnet:?xt=urn:btih:dd8255ecdc7ca55fb0bbf81323d87062db1f6d1c&dn=Big+Buck+Bunny&tr=udp%3A%2F%2Fexplodie.org%3A6969&tr=udp%3A%2F%2Ftracker.coppersurfer.tk%3A6969&tr=udp%3A%2F%2Ftracker.empire-js.us%3A1337&tr=udp%3A%2F%2Ftracker.leechers-paradise.org%3A6969&tr=udp%3A%2F%2Ftracker.opentrackr.org%3A1337&tr=wss%3A%2F%2Ftracker.btorrent.xyz&tr=wss%3A%2F%2Ftracker.fastcast.nz&tr=wss%3A%2F%2Ftracker.openwebtorrent.com&ws=https%3A%2F%2Fwebtorrent.io%2Ftorrents%2F&xs=https%3A%2F%2Fwebtorrent.io%2Ftorrents%2Fbig-buck-bunny.torrent";

            var result = await client.Magnet.UploadMagnetAsync(magnet);

            Assert.Equal("dd8255ecdc7ca55fb0bbf81323d87062db1f6d1c", result.Hash);
        }

        [Fact]
        public async Task UploadFile()
        {
            var client = new AllDebridNETClient("AllDebridNETTest", Setup.ApiKey);

            const String filePath = @"big-buck-bunny.torrent";

            var file = await System.IO.File.ReadAllBytesAsync(filePath);

            var result = await client.Magnet.UploadFileAsync(file);

            Assert.Equal("dd8255ecdc7ca55fb0bbf81323d87062db1f6d1c", result.Hash);
        }
        
        [Fact]
        public async Task Status()
        {
            var client = new AllDebridNETClient("AllDebridNETTest", Setup.ApiKey);

            var result = await client.Magnet.StatusAllAsync();
        }

        [Fact]
        public async Task Status_Single()
        {
            var client = new AllDebridNETClient("AllDebridNETTest", Setup.ApiKey);

            var result = await client.Magnet.StatusAsync("123233471");
        }

        [Fact]
        public async Task Delete()
        {
            var client = new AllDebridNETClient("AllDebridNETTest", Setup.ApiKey);

            await client.Magnet.DeleteAsyc("123233471");
        }

        [Fact]
        public async Task RestartAsync()
        {
            var client = new AllDebridNETClient("AllDebridNETTest", Setup.ApiKey);

            await client.Magnet.RestartAsync("123233471");
        }
        
        [Fact]
        public async Task InstantAvailability()
        {
            var client = new AllDebridNETClient("AllDebridNETTest", Setup.ApiKey);

            const String magnet = "magnet:?xt=urn:btih:dd8255ecdc7ca55fb0bbf81323d87062db1f6d1c&dn=Big+Buck+Bunny&tr=udp%3A%2F%2Fexplodie.org%3A6969&tr=udp%3A%2F%2Ftracker.coppersurfer.tk%3A6969&tr=udp%3A%2F%2Ftracker.empire-js.us%3A1337&tr=udp%3A%2F%2Ftracker.leechers-paradise.org%3A6969&tr=udp%3A%2F%2Ftracker.opentrackr.org%3A1337&tr=wss%3A%2F%2Ftracker.btorrent.xyz&tr=wss%3A%2F%2Ftracker.fastcast.nz&tr=wss%3A%2F%2Ftracker.openwebtorrent.com&ws=https%3A%2F%2Fwebtorrent.io%2Ftorrents%2F&xs=https%3A%2F%2Fwebtorrent.io%2Ftorrents%2Fbig-buck-bunny.torrent";

            var result = await client.Magnet.InstantAvailabilityAsync(magnet);
        }
    }
}
