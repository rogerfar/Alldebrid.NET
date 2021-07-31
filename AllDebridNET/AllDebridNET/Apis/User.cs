using System;
using System.Collections.Generic;
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
        ///     This endpoint clears a user notification with its code. Current notifications codes can be retreive from the /user endpoint.
        /// </summary>
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of
        ///     cancellation.
        /// </param>
        public async Task<User> GetAsync(CancellationToken cancellationToken = default)
        {
            var user = await _requests.GetRequestAsync<UserResponse>("user", true, null, cancellationToken);

            return user.User;
        }

        /// <summary>
        ///     Use this endpoint to get user informations.
        /// </summary>
        /// <param name="code">
        ///     Notification code to clear.
        /// </param>
        /// <param name="cancellationToken">
        ///     A cancellation token that can be used by other objects or threads to receive notice of
        ///     cancellation.
        /// </param>
        public async Task<UserNotificationClear> ClearNotificationsAsync(String code, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<String, String>
            {
                {
                    "code", code
                }
            };

            return await _requests.GetRequestAsync<UserNotificationClear>("user/notification/clear", true, parameters, cancellationToken);
        }
    }
}
