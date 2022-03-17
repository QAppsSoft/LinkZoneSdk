using FluentResults;

namespace LinkZoneSdk.Errors.Password
{
    public abstract class PasswordChangeError : Error
    {
        public PasswordChangeError(string userName, string currentPassword, string newPassword)
        {
            UserName = userName;
            CurrentPassword = currentPassword;
            NewPassword = newPassword;
        }

        public string UserName { get; }
        public string CurrentPassword { get; }
        public string NewPassword { get; }
    }
}