namespace LinkZoneSdk.Errors
{
    public static class ErrorCodes
    {
        public const string InvalidJsonDataReceived = "101";
        public const string HttpRequestException = "102";

        public const string WrongUserOrPassword = "010101";
        public const string LoginFailed = "010102";
        public const string LoginAttemptsExceeded = "010103";

        public const string PasswordChangeFailed = "010401";
        public const string WrongCurrentPassword = "010402";
    }
}
