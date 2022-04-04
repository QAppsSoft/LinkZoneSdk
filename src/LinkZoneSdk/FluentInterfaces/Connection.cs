using FluentResults;
using LinkZoneSdk.Models.Connection;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LinkZoneSdk
{
    internal sealed partial class Sdk : IConnection
    {
        public async Task<Result<ConnectionState>> GetConnectionState(CancellationToken? cancellation = null)
        {
            var result = await _apiService
                .RequestJsonRpcAsync<ConnectionState, Dictionary<string, object>>("GetConnectionState", "3.1", null,
                    cancellation).ConfigureAwait(false);

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

        public async Task<Result<bool>> Connect(CancellationToken? cancellation = null)
        {
            var result = await _apiService
                .RequestJsonRpcAsync<ConnectionState, Dictionary<string, object>>("Connect", "3.2", null, cancellation)
                .ConfigureAwait(false);

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

        public async Task<Result<bool>> Disconnect(CancellationToken? cancellation = null)
        {
            var result = await _apiService
                .RequestJsonRpcAsync<ConnectionState, Dictionary<string, object>>("DisConnect", "3.3", null,
                    cancellation).ConfigureAwait(false);

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
