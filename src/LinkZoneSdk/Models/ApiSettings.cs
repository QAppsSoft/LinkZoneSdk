using System;
using System.Net;

namespace LinkZoneSdk.Models
{
    public readonly struct ApiSettings
    {
        private const string DefaultAddress = "192.168.1.1";
        private static readonly ApiSettings DefaultSettings;
        private const int DefaultTimeout = 30;

        static ApiSettings()
        {
            DefaultSettings = new ApiSettings(IPAddress.Parse(DefaultAddress), TimeSpan.FromSeconds(DefaultTimeout));
        }

        public ApiSettings(IPAddress address, TimeSpan timeout)
        {
            Address = address;
            Timeout = timeout;
        }

        public IPAddress Address { get; }
        public TimeSpan Timeout { get; }

        public static ApiSettings Default()
        {
            return DefaultSettings;
        }
    }
}