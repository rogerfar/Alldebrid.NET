using Newtonsoft.Json;

namespace AllDebridNET;

internal class UserResponse
{
    [JsonProperty("user")]
    public User? User { get; set; }
}

public class User
{
    /// <summary>
    ///     User username.
    /// </summary>
    [JsonProperty("username")]
    public String? Username { get; set; }

    /// <summary>
    ///     User email.
    /// </summary>
    [JsonProperty("email")]
    public String? Email { get; set; }

    /// <summary>
    ///     true is premium, false if not.
    /// </summary>
    [JsonProperty("isPremium")]
    public Boolean IsPremium { get; set; }

    /// <summary>
    ///     true is user has active subscription, false if not.
    /// </summary>
    [JsonProperty("isSubscribed")]
    public Boolean IsSubscribed { get; set; }

    /// <summary>
    ///     true is account is in freedays trial, false if not.
    /// </summary>
    [JsonProperty("isTrial")]
    public Boolean IsTrial { get; set; }

    /// <summary>
    ///     0 if user is not premium, or timestamp until user is premium.
    /// </summary>
    [JsonProperty("premiumUntil")]
    public Int64 PremiumUntil { get; set; }

    /// <summary>
    ///     Language used by the user on Alldebrid, eg. 'en', 'fr'. Default to fr.
    /// </summary>
    [JsonProperty("lang")]
    public String? Lang { get; set; }

    /// <summary>
    ///     Preferer TLD used by the user, eg. 'fr', 'es'. Default to fr.
    /// </summary>
    [JsonProperty("preferedDomain")]
    public String? PreferedDomain { get; set; }

    /// <summary>
    ///     Number of fidelity points.
    /// </summary>
    [JsonProperty("fidelityPoints")]
    public Int64 FidelityPoints { get; set; }

    /// <summary>
    ///     Remaining quotas for the limited hosts (in MB).
    /// </summary>
    [JsonProperty("limitedHostersQuotas")]
    public Dictionary<String, Int64>? LimitedHostersQuotas { get; set; }

    /// <summary>
    ///     When in trial mode, remaining global traffic quota available (in MB).
    /// </summary>
    [JsonProperty("remainingTrialQuota")]
    public Int64 RemainingTrialQuota { get; set; }

    [JsonProperty("notifications")]
    public List<String>? Notifications { get; set; }
}