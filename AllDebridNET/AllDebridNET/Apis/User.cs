using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AllDebridNET.Apis
{
    public class UserApi
    {
        private readonly Requests _requests;

        internal UserApi(HttpClient httpClient, Store store)
        {
            _requests = new Requests(httpClient, store);
        }

        /// <summary>
        ///     Use this endpoint to get user informations.
        /// </summary>
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of
        ///     cancellation.
        /// </param>
        /// <returns>
        ///     The currently logged in user.
        /// </returns>
        public async Task<User> GetAsync(CancellationToken cancellationToken = default)
        {
            var user = await _requests.GetRequestAsync<UserResponse>("user", true, null, cancellationToken);

            return user.User;
        }
    }
}
