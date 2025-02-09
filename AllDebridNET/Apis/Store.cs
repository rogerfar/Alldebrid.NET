namespace AllDebridNET;

internal class Store
{
    public const String API_URL = "https://api.alldebrid.com/";
    public const String API_VERSION = "v4.1/";

    public String? Agent { get; set; }
    public String? ApiKey { get; set; }
}