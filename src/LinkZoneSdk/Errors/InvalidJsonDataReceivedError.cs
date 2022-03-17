using FluentResults;

namespace LinkZoneSdk.Errors
{
    public class InvalidJsonDataReceivedError : Error
    {
        public InvalidJsonDataReceivedError(string dataReceived)
            : base("The Json data received from server is invalid")
        {
            DataReceived = dataReceived;
            Metadata.Add("ErrorCode", ErrorCodes.InvalidJsonDataReceived);
        }

        public string DataReceived { get; }
    }
}