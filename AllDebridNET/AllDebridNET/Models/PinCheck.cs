using System;
using Newtonsoft.Json;

namespace AllDebridNET
{
    public class PinCheck
    {
        /// <summary>
        ///     Auth apikey, available once user has submitted the pin code.
        /// </summary>
        [JsonProperty("apikey")]
        public String Apikey { get; set; }

        /// <summary>
        ///     false if user didn't enter the pin on website yet.
        /// </summary>
        [JsonProperty("activated")]
        public Boolean Activated { get; set; }

        /// <summary>
        ///     Seconds left before PIN expires
        /// </summary>
        [JsonProperty("expires_in")]
        public Int64 ExpiresIn { get; set; }
    }
}
