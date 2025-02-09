using System.Text.Json.Serialization;

namespace AllDebridNET;

public class VerifyEmailStatusResponse
{
    /// <summary>
    ///     Verification status, either waiting, allowed or denied
    /// </summary>
    [JsonPropertyName("verif")]
    public String? VerifyEmailStatus { get; set; }

    /// <summary>
    ///     Whether the verification email is resensable with /user/verif/resend, returned when verif = waiting
    /// </summary>
    [JsonPropertyName("resendable")]
    public Boolean? Resendable { get; set; }

    /// <summary>
    ///     The apikey, returned when verif = allowed
    /// </summary>
    [JsonPropertyName("apikey")]
    public String? ApiKey { get; set; }
}
