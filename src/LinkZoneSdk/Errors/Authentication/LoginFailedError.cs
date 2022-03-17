namespace LinkZoneSdk.Errors.Authentication
{
    public class LoginFailedError : AuthenticationError
    {
        public LoginFailedError(string password) : this("admin", password)
        {
        }

        public LoginFailedError(string userName, string password) : base(userName, password)
        {
            Message = "Failed to login, maybe some one already did";
            Metadata.Add("ErrorCode", ErrorCodes.LoginFailed);
        }
    }
}