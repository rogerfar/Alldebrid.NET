using System.Threading.Tasks;
using RDNET.Test;
using Xunit;

namespace AllDebridNET.Test
{
    public class UsersTest
    {
        [Fact]
        public async Task Hosts()
        {
            var client = new AllDebridNETClient("AllDebridNETTest", Setup.ApiKey);

            var result = await client.User.GetAsync();

            Assert.True(result.Email.Contains("@"));
        }
    }
}
