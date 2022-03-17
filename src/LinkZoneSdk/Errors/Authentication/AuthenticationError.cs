using FluentResults;

namespace LinkZoneSdk.Errors.Authentication
{
    public abstract class AuthenticationError : Error
    {
        public AuthenticationError(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public string UserName { get; }
        public string Password { get; }
    }
}