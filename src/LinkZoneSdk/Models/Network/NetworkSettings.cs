using System.Text.Json.Serialization;
using LinkZoneSdk.Enums;

namespace LinkZoneSdk.Models.Network
{
    public sealed class NetworkSettings
    {
        [JsonConstructor]
        public NetworkSettings(NetworkMode networkMode, int netselectionMode, int networkBand, int domesticRoam, int domesticRoamGuard)
        {
            this.NetworkMode = networkMode;
            this.NetselectionMode = netselectionMode;
            this.NetworkBand = networkBand;
            this.DomesticRoam = domesticRoam;
            this.DomesticRoamGuard = domesticRoamGuard;
        }

        [JsonPropertyName("NetworkMode")]
        public NetworkMode NetworkMode { get; }

        [JsonPropertyName("NetselectionMode")]
        public int NetselectionMode { get; }

        [JsonPropertyName("NetworkBand")]
        public int NetworkBand { get; }

        [JsonPropertyName("DomesticRoam")]
        public int DomesticRoam { get; }

        [JsonPropertyName("DomesticRoamGuard")]
        public int DomesticRoamGuard { get; }
    }
}
