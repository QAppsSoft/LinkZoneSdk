using System.Text.Json.Serialization;
using LinkZoneSdk.Enums;

namespace LinkZoneSdk.Models.Ussd
{
    public class UssdData
    {
        [JsonConstructor]
        public UssdData(string ussdContent, int ussdType)
        {
            UssdContent = ussdContent;
            UssdType = ussdType;
        }

        [JsonPropertyName("UssdContent")]
        public string UssdContent { get; }

        [JsonPropertyName("UssdType")]
        public int UssdType { get; }
    }
}