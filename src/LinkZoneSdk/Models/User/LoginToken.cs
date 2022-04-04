using System.Text.Json.Serialization;

namespace LinkZoneSdk.Models.User
{
    public sealed class LoginToken
    {
        [JsonConstructor]
        public LoginToken(int token)
        {
            Token = token;
        }

        [JsonPropertyName("token")]
        public int Token { get; }
    }
}
