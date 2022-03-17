namespace LinkZoneSdk.Errors.Authentication
{
    public class LoginAttemptsExceededError : AuthenticationError
    {
        public LoginAttemptsExceededError(string password) : this("admin", password)
        {
        }

        public LoginAttemptsExceededError(string userName, string password) : base(userName, password)
        {
            Message = "Login attempts limit exceeded";
            Metadata.Add("ErrorCode", ErrorCodes.LoginAttemptsExceeded);
        }
    }
}