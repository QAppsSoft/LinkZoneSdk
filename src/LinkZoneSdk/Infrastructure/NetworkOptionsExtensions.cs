using System;
using System.Net;
using LinkZoneSdk.Models;

namespace LinkZoneSdk.Infrastructure
{
    public static class NetworkOptionsExtensions
    {
        public static ApiSettings WithTimeout(this ApiSettings apiSettings, TimeSpan timeout)
        {
            return new ApiSettings(apiSettings.Address, timeout);
        }

        public static ApiSettings WithIpAddress(this ApiSettings apiSettings, IPAddress address)
        {
            return new ApiSettings(address, apiSettings.Timeout);
        }
    }
}
