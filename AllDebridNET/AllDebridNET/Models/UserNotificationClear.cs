using System;
using Newtonsoft.Json;

namespace AllDebridNET
{
    public class UserNotificationClear
    {
        [JsonProperty("message")]
        public String Message { get; set; }
    }
}
