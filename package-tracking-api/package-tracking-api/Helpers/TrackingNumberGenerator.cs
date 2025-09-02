namespace package_tracking_api.Helpers;

public static class TrackingNumberGenerator
{
    public static string Generate()
    {
        return "P" + DateTime.Now.ToString("MMddHHmmss") + 
               Guid.NewGuid().ToString("N").Substring(0,4).ToUpper();
    }
}