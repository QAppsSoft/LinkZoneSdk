namespace LinkZoneSdk.Errors.Password
{
    public class WrongCurrentPasswordError : PasswordChangeError
    {
        public WrongCurrentPasswordError(string userName, string currentPassword, string newPassword) : base(userName, currentPassword, newPassword)
        {
            Message = "The supplied current password is wrong";
            Metadata.Add("ErrorCode", ErrorCodes.PasswordChangeFailed);
        }
    }
}