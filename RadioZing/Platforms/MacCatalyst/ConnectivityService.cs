using System.Net.NetworkInformation;

namespace RadioZing.Platforms.MacCatalyst;

public class ConnectivityService
{
    public ConnectivityService()
    {
    }

    public async Task<bool> IsConnected()
    {
        bool result = false;
        try
        {
            var hostUrl = "www.google.com";

            Ping ping = new Ping();

            PingReply pingReply = await ping.SendPingAsync(hostUrl);
            result = pingReply.Status == IPStatus.Success;
        }
        catch
        {
            result = false;
        }
        return result;
    }
}
