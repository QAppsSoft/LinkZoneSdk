using LinkZoneSdk.Models;
using System.Net;

namespace LinkZoneSdk
{
    internal partial class Sdk : ISdk
    {
        private readonly ApiService _apiService;

        internal static IPAddress DefaultAddress { get; private set; } = IPAddress.Parse("192.168.1.1");

        public Sdk(ApiService apiService)
        {
            _apiService = apiService;
        }

        public static void Configure(IPAddress address)
        {
            DefaultAddress = address;
        }

        private static bool RequestJsonRpcIsOk<TResult, TError>(ResultData<TResult, TError> resultData)
            where TResult : class
            where TError : class
        {
            return resultData.ResultParameters != null &&
                   resultData.ErrorParameters == null;
        }

        private static bool RequestJsonRpcIsAbsoluteOk<TResult, TError>(ResultData<TResult, TError>? resultData)
            where TResult : class
            where TError : class
        {
            return resultData != null &&
                   resultData.ResultParameters != null &&
                   resultData.ErrorParameters == null;
        }

        public IUser User()
        {
            return this;
        }

        public IConnection Connection()
        {
            return this;
        }
    }
}