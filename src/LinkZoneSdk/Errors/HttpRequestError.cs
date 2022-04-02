using FluentResults;

namespace LinkZoneSdk.Errors
{
    public class HttpRequestError : Error
    {
        public HttpRequestError() : base("An exception has been throw when executing an Http request")
        {
            Metadata.Add("ErrorCode", ErrorCodes.HttpRequestException);
        }
    }
}
