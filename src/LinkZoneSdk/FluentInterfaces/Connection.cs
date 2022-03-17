using FluentResults;
using LinkZoneSdk.Models.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LinkZoneSdk
{
    internal partial class Sdk : IConnection
    {
        public async Task<Result<ConnectionState>> GetConnectionState()
        {
            var result = await _apiService.RequestJsonRpcAsync<ConnectionState, Dictionary<string, object>>("GetConnectionState", "3.1");

            if (result.IsFailed)
            {
                return result.ToResult();
            }

            var resultValue = result.Value;

            if (RequestJsonRpcIsOk(resultValue))
            {
                return Result.Ok(resultValue.ResultParameters);
            }

            return Result.Fail("Failed to retrieve connection status");
        }

        public async Task<Result<bool>> Connect()
        {
            var result = await _apiService.RequestJsonRpcAsync<ConnectionState, Dictionary<string, object>>("Connect", "3.2");

            if (result.IsFailed)
            {
                return result.ToResult();
            }

            var resultValue = result.Value;

            if (RequestJsonRpcIsOk(resultValue))
            {
                return Result.Ok(true);
            }

            return Result.Ok(false);
        }

        public async Task<Result<bool>> Disconnect()
        {
            var result = await _apiService.RequestJsonRpcAsync<ConnectionState, Dictionary<string, object>>("DisConnect", "3.3");

            if (result.IsFailed)
            {
                return result.ToResult();
            }

            var resultValue = result.Value;

            if (RequestJsonRpcIsOk(resultValue))
            {
                return Result.Ok(true);
            }

            return Result.Ok(false);
        }
    }
}
