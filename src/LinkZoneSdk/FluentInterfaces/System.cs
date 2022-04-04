using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using LinkZoneSdk.Models.System;

namespace LinkZoneSdk
{
    internal sealed partial class Sdk : ISystem
    {
        public async Task<Result<Dictionary<string, object>>> GetInfo(CancellationToken? cancellation = null)
        {
            var result = await _apiService.RequestJsonRpcAsync<Dictionary<string, object>, Dictionary<string, object>>("GetSystemInfo", "13.1", null, cancellation);

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

        public async Task<Result<SystemStatus>> GetStatus(CancellationToken? cancellation = null)
        {
            var result = await _apiService.RequestJsonRpcAsync<SystemStatus, Dictionary<string, object>>("GetSystemStatus", "13.4", null, cancellation);

            if (result.IsFailed)
            {
                return result.ToResult();
            }

            var resultValue = result.Value;

            if (RequestJsonRpcIsOk(resultValue))
            {
                return Result.Ok(resultValue.ResultParameters);
            }

            return Result.Fail("Failed to retrieve system status");
        }


        public async Task<Result<bool>> RebootDevice(CancellationToken? cancellation = null)
        {
            var result = await _apiService.RequestJsonRpcAsync<Dictionary<string, object>, Dictionary<string, object>>("SetDeviceReboot", "13.5", null, cancellation);

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