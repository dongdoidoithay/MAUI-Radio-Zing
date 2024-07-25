namespace RadioZing;

public static class Config
{
    public static bool ListenTogetherIsVisible => true;

    public static bool Desktop
    {
        get
        {
#if WINDOWS || MACCATALYST
            return true;
#else
            return false;
#endif
        }
    }

    public static string BaseWeb = $"https://dotnetpodcasts.azurewebsites.net/";
    public static string APIUrl = $"https://api.novelfull.us";
    public static string ListenTogetherUrl = $"https://dotnetpodcasts-listentogether-hub.azurewebsites.net/listentogether";

}
