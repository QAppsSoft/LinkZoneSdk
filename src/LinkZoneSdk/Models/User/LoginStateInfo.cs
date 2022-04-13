using LinkZoneSdk.Enums;
using System.Text.Json.Serialization;

namespace LinkZoneSdk.Models.User
{
    public sealed class LoginStateInfo
    {
        [JsonConstructor]
        public LoginStateInfo(LoginState state, int loginRemainingTimes, int lockedRemainingTime
        )
        {
            State = state;
            LoginRemainingTimes = loginRemainingTimes;
            LockedRemainingTime = lockedRemainingTime;
        }

        [JsonPropertyName("State")]
        public LoginState State { get; }

        [JsonPropertyName("LoginRemainingTimes")]
        public int LoginRemainingTimes { get; }

        [JsonPropertyName("LockedRemainingTime")]
        public int LockedRemainingTime { get; }
    }
}
