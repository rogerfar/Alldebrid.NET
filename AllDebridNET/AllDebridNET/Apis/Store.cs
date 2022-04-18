using System;

namespace AllDebridNET.Apis;

internal class Store
{
    public String ApiUrl = "http://api.alldebrid.com/v4/";
        
    public String Agent { get; set; }
    public String ApiKey { get; set; }
}