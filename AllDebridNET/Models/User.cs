using System.Text.Json.Serialization;

namespace AllDebridNET;

internal class UserResponse
{
    [JsonPropertyName("user")]
    public User? User { get; set; }
}

public class User
{
    /// <summary>
    ///     User username.
    /// </summary>
    [JsonPropertyName("username")]
    public String? Username { get; set; }

    /// <summary>
    ///     User email.
    /// </summary>
    [JsonPropertyName("email")]
    public String? Email { get; set; }

    /// <summary>
    ///     true is premium, false if not.
    /// </summary>
    [JsonPropertyName("isPremium")]
    public Boolean IsPremium { get; set; }

    /// <summary>
    ///     true is user has active subscription, false if not.
    /// </summary>
    [JsonPropertyName("isSubscribed")]
    public Boolean IsSubscribed { get; set; }

    /// <summary>
    ///     true is account is in freedays trial, false if not.
    /// </summary>
    [JsonPropertyName("isTrial")]
    public Boolean IsTrial { get; set; }

    /// <summary>
    ///     0 if user is not premium, or timestamp until user is premium.
    /// </summary>
    [JsonPropertyName("premiumUntil")]
    public Int64 PremiumUntil { get; set; }

    /// <summary>
    ///     Language used by the user on Alldebrid, eg. 'en', 'fr'. Default to fr.
    /// </summary>
    [JsonPropertyName("lang")]
    public String? Lang { get; set; }

    /// <summary>
    ///     Preferer TLD used by the user, eg. 'fr', 'es'. Default to fr.
    /// </summary>
    [JsonPropertyName("preferedDomain")]
    public String? PreferedDomain { get; set; }

    /// <summary>
    ///     Number of fidelity points.
    /// </summary>
    [JsonPropertyName("fidelityPoints")]
    public Int64 FidelityPoints { get; set; }

    /// <summary>
    ///     Remaining quotas for the limited hosts (in MB).
    /// </summary>
    [JsonPropertyName("limitedHostersQuotas")]
    public Dictionary<String, Int64>? LimitedHostersQuotas { get; set; }

    /// <summary>
    ///     When in trial mode, remaining global traffic quota available (in MB).
    /// </summary>
    [JsonPropertyName("remainingTrialQuota")]
    public Int64 RemainingTrialQuota { get; set; }

    [JsonPropertyName("notifications")]
    public List<String>? Notifications { get; set; }
}