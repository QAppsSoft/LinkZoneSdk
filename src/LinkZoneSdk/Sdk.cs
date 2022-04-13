using LinkZoneSdk.Models;
using System.Net;

namespace LinkZoneSdk
{
    internal sealed partial class Sdk : ISdk
    {
        private readonly IApiService _apiService;
        private static ApiSettings _apiSettings;

        internal static IPAddress Address => _apiSettings.Address;

        static Sdk()
        {
            _apiSettings = ApiSettings.Default();
        }

        public Sdk(IApiService apiService)
        {
            _apiService = apiService;
        }

        public static void ToDefaults()
        {
            _apiSettings = ApiSettings.Default();
        }
        
        public static void Configure(ApiSettings apiSettings)
        {
            _apiSettings = apiSettings;
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

        public ISystem System()
        {
            return this;
        }

        public INetwork Network()
        {
            return this;
        }
    }
}