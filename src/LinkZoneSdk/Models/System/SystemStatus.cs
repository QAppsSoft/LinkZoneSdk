using System.Text.Json.Serialization;
using LinkZoneSdk.Enums;
using LinkZoneSdk.JsonConverters;

namespace LinkZoneSdk.Models.System
{
    public sealed class SystemStatus
    {
        [JsonConstructor]
        public SystemStatus(
            ChargeState chargeState,
            int batCap,
            int batLevel,
            int smsState,
            int connectionStatus,
            int conprofileerror,
            int clearCode,
            int mpdpRejectCount,
            string networkType,
            string networkName,
            int roaming,
            int domesticRoaming,
            int signalStrength,
            int wlanState,
            int currNum,
            int totalConnNum
        )
        {
            this.ChargeState = chargeState;
            this.BatCap = batCap;
            this.BatLevel = batLevel;
            this.SmsState = smsState;
            this.ConnectionStatus = connectionStatus;
            this.Conprofileerror = conprofileerror;
            this.ClearCode = clearCode;
            this.MPdpRejectCount = mpdpRejectCount;
            this.NetworkType = networkType;
            this.NetworkName = networkName;
            this.Roaming = roaming;
            this.DomesticRoaming = domesticRoaming;
            this.SignalStrength = signalStrength;
            this.WlanState = wlanState;
            this.CurrNum = currNum;
            this.TotalConnNum = totalConnNum;
        }

        [JsonPropertyName("chg_state")]
        public ChargeState ChargeState { get; }

        [JsonPropertyName("bat_cap")]
        public int BatCap { get; }

        [JsonPropertyName("bat_level")]
        public int BatLevel { get; }

        [JsonPropertyName("SmsState")]
        public int SmsState { get; }

        [JsonPropertyName("ConnectionStatus")]
        public int ConnectionStatus { get; }

        [JsonPropertyName("Conprofileerror")]
        public int Conprofileerror { get; }

        [JsonPropertyName("ClearCode")]
        public int ClearCode { get; }

        [JsonPropertyName("mPdpRejectCount")]
        public int MPdpRejectCount { get; }

        [JsonConverter(typeof(NetworkTypeJsonConverter))]
        [JsonPropertyName("NetworkType")]
        public string NetworkType { get; }

        [JsonPropertyName("NetworkName")]
        public string NetworkName { get; }

        [JsonPropertyName("Roaming")]
        public int Roaming { get; }

        [JsonPropertyName("Domestic_Roaming")]
        public int DomesticRoaming { get; }

        [JsonPropertyName("SignalStrength")]
        public int SignalStrength { get; }

        [JsonPropertyName("WlanState")]
        public int WlanState { get; }

        [JsonPropertyName("curr_num")]
        public int CurrNum { get; }

        [JsonPropertyName("TotalConnNum")]
        public int TotalConnNum { get; }
    }
}
