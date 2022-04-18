using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AllDebridNET;

public class Hosts
{
    [JsonProperty("hosts")]
    public Dictionary<String, Host> HostList { get; set; }

    [JsonProperty("streams")]
    public Dictionary<String, Host> Streams { get; set; }

    [JsonProperty("redirectors")]
    public Dictionary<String, Host> Redirectors { get; set; }
}

public class Host
{
    /// <summary>
    ///     Host name.
    /// </summary>
    [JsonProperty("name")]
    public String Name { get; set; }

    /// <summary>
    ///     Either "premium" or "free". Premium hosts need a premium subscription.
    /// </summary>
    [JsonProperty("type")]
    public String Type { get; set; }

    /// <summary>
    ///     Host domains.
    /// </summary>
    [JsonProperty("domains")]
    public List<String> Domains { get; set; }

    /// <summary>
    ///     Rgexp matching link format that Alldebrid supports.
    /// </summary>
    [JsonProperty("regexps")]
    public List<String> Regexps { get; set; }

    [JsonProperty("regexp")]
    public String Regexp { get; set; }

    /// <summary>
    ///     Is the host currently (less than 5 min) working on Alldebrid (only tested on some hosts, updated every ~ 10 min).
    /// </summary>
    [JsonProperty("status")]
    public Boolean? Status { get; set; }

    [JsonProperty("hardRedirect")]
    public List<String> HardRedirect { get; set; }
}

internal class HostsPriorityResponse
{
    [JsonProperty("hosts")]
    public Dictionary<String, Int64> Hosts { get; set; }
}