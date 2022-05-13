namespace AllDebridNET;

internal class InstantAvailabilityResponse
{
    public List<InstantAvailabilityResponseMagnet>? Magnets { get; set; }
}

internal class InstantAvailabilityResponseMagnet
{
    public String? Magnet { get; set; }
    public String? Hash { get; set; }
    public Boolean Instant { get; set; }
}