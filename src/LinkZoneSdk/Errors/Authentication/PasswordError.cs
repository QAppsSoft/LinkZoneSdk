namespace LinkZoneSdk.Errors.Authentication
{
    public class PasswordError : AuthenticationError
    {
        public PasswordError(string password) : this("admin", password)
        {
        }

        public PasswordError(string userName, string password) : base(userName, password)
        {
            Message = "Username or password is not correct";
            Metadata.Add("ErrorCode", ErrorCodes.WrongUserOrPassword);
        }
    }
}