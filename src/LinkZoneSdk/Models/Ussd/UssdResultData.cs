using System.Text.Json.Serialization;

namespace LinkZoneSdk.Models.Ussd
{
    public class UssdResultData
    {
        [JsonConstructor]
        public UssdResultData(int sendState, int ussdType, string ussdContent, int ussdContentLen)
        {
            SendState = sendState;
            UssdType = ussdType;
            UssdContent = ussdContent;
            UssdContentLen = ussdContentLen;
        }

        [JsonPropertyName("SendState")]
        public int SendState { get; }

        [JsonPropertyName("UssdType")]
        public int UssdType { get; }

        [JsonPropertyName("UssdContent")]
        public string UssdContent { get; }

        [JsonPropertyName("UssdContentLen")]
        public int UssdContentLen { get; }
    }
}
