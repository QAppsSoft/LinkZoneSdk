using LinkZoneSdk.Enums;
using System.Text.Json.Serialization;

namespace LinkZoneSdk.Models.Connection
{
    public class ConnectionState
    {
        [JsonConstructor]
        public ConnectionState(NetworkStatus connectionStatus,
            int conprofileerror,
            int clearCode,
            int mpdpRejectCount,
            string ipv4Adrress,
            string ipv6Adrress,
            int speedDl,
            int speedUl,
            int dlRate,
            int ulRate,
            int connectionTime,
            int ulBytes,
            int dlBytes
        )
        {
            ConnectionStatus = connectionStatus;
            Conprofileerror = conprofileerror;
            ClearCode = clearCode;
            MPdpRejectCount = mpdpRejectCount;
            IPv4Adrress = ipv4Adrress;
            IPv6Adrress = ipv6Adrress;
            SpeedDl = speedDl;
            SpeedUl = speedUl;
            DlRate = dlRate;
            UlRate = ulRate;
            ConnectionTime = connectionTime;
            UlBytes = ulBytes;
            DlBytes = dlBytes;
        }

        [JsonPropertyName("ConnectionStatus")]
        public NetworkStatus ConnectionStatus { get; }

        [JsonPropertyName("Conprofileerror")]
        public int Conprofileerror { get; }

        [JsonPropertyName("ClearCode")]
        public int ClearCode { get; }

        [JsonPropertyName("mPdpRejectCount")]
        public int MPdpRejectCount { get; }

        [JsonPropertyName("IPv4Adrress")]
        public string IPv4Adrress { get; }

        [JsonPropertyName("IPv6Adrress")]
        public string IPv6Adrress { get; }

        [JsonPropertyName("Speed_Dl")]
        public int SpeedDl { get; }

        [JsonPropertyName("Speed_Ul")]
        public int SpeedUl { get; }

        [JsonPropertyName("DlRate")]
        public int DlRate { get; }

        [JsonPropertyName("UlRate")]
        public int UlRate { get; }

        [JsonPropertyName("ConnectionTime")]
        public int ConnectionTime { get; }

        [JsonPropertyName("UlBytes")]
        public int UlBytes { get; }

        [JsonPropertyName("DlBytes")]
        public int DlBytes { get; }
    }
}