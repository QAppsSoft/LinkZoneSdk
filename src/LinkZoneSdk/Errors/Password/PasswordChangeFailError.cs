namespace LinkZoneSdk.Errors.Password
{
    public class PasswordChangeFailError : PasswordChangeError
    {
        public PasswordChangeFailError(string userName, string currentPassword, string newPassword) : base(userName, currentPassword, newPassword)
        {
            Message = "Failed to change current password";
            Metadata.Add("ErrorCode", ErrorCodes.PasswordChangeFailed);
        }
    }
}