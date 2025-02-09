using AllDebridNET.Models;

namespace AllDebridNET;

public class UserApi
{
    private readonly Requests _requests;

    internal UserApi(HttpClient httpClient, Store store)
    {
        _requests = new(httpClient, store);
    }

    /// <summary>
    ///     Use this endpoint to get user informations.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    public async Task<User?> GetAsync(CancellationToken cancellationToken = default)
    {
        var user = await _requests.GetRequestAsync<UserResponse>("user", true, null, cancellationToken);

        return user.User;
    }

    /// <summary>
    ///     This endpoint clears a user notification with its code. Current notifications codes can be retreive from the /user endpoint.
    /// </summary>
    /// <param name="code">
    ///     Notification code to clear.
    /// </param>
    /// <param name="cancellationToken"></param>
    public async Task<ActionResponse> ClearNotificationsAsync(String code, CancellationToken cancellationToken = default)
    {
        var parameters = new Dictionary<String, String>
        {
            {
                "code", code
            }
        };

        return await _requests.GetRequestAsync<ActionResponse>("user/notification/clear", true, parameters, cancellationToken);
    }

    /// <summary>
    ///    When connecting from a new place, you may be asked to verify this new connection for security measures. This include the API use.
    ///    If you trigguer such security, an email is sent to the user to confirm the new location. In those case, the api returns an AUTH_BLOCKED error, along with an token parameter as documented in the code example.
    ///    You can use this endpoint to track the status of the new connexion validation, and retreive the apikey when the request has been confirmed by email.
    /// </summary>
    /// <param name="token">Verification token returned along with the AUTH_BLOCKED error</param>
    /// <param name="cancellationToken"></param>
    public async Task<VerifyEmailStatusResponse> VerifyEmailStatusAsync(String token, CancellationToken cancellationToken = default)
    {
        var data = new[]
        {
            new KeyValuePair<String, String>("token", token)
        };

        return await _requests.PostRequestAsync<VerifyEmailStatusResponse>("user/verif", data, true, cancellationToken);
    }

    /// <summary>
    ///    Allow to send again the verification email, allowed once. We can referrer to the resendable property of the /user/verif response to know if you can request a resend.
    /// </summary>
    /// <param name="token">Verification token returned along with the AUTH_BLOCKED error</param>
    /// <param name="cancellationToken"></param>
    public async Task<ResendEmailResponse> ResendEmailNotificationAsync(String token, CancellationToken cancellationToken = default)
    {
        var data = new[]
        {
            new KeyValuePair<String, String>("token", token)
        };

        return await _requests.PostRequestAsync<ResendEmailResponse>("user/verif/resend", data, true, cancellationToken);
    }
}