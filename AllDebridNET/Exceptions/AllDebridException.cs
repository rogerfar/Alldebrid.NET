namespace AllDebridNET;

public class AllDebridException(String error, String errorCode) : Exception(GetMessage(error, errorCode))
{
    public String ServerError { get; } = error;
    public String ErrorCode { get; } = errorCode;
    public String Error { get; } = GetMessage(error, errorCode);

    private static String GetMessage(String error, String errorCode)
    {
        return $"{error} ({errorCode})";
    }
}