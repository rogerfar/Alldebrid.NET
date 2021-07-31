using System;
using Newtonsoft.Json;

namespace AllDebridNET
{
    public class PinRequest
    {
        /// <summary>
        ///     Pin code to display to your user.
        /// </summary>
        [JsonProperty("pin")]
        public String Pin { get; set; }

        /// <summary>
        ///     Hash needed for the pin/check call.
        /// </summary>
        [JsonProperty("check")]
        public String Check { get; set; }

        /// <summary>
        ///     Number of second before the code expires.
        /// </summary>
        [JsonProperty("expires_in")]
        public Int64 ExpiresIn { get; set; }

        /// <summary>
        ///     Url to display with PIN included.
        /// </summary>
        [JsonProperty("user_url")]
        public String UserUrl { get; set; }

        /// <summary>
        ///     Base url.
        /// </summary>
        [JsonProperty("base_url")]
        public String BaseUrl { get; set; }

        /// <summary>
        ///     Endpoint to pool to get auth apikey once user submitted the PIN code.
        /// </summary>
        [JsonProperty("check_url")]
        public String CheckUrl { get; set; }
    }
}
