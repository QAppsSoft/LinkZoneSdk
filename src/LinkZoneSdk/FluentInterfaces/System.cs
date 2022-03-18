using System.Collections.Generic;
using System.Threading.Tasks;
using FluentResults;
using LinkZoneSdk.Models.System;

namespace LinkZoneSdk
{
    internal partial class Sdk : ISystem
    {
        public async Task<Result<Dictionary<string, object>>> GetSystemInfo()
        {
            var result = await _apiService.RequestJsonRpcAsync<Dictionary<string, object>, Dictionary<string, object>>("GetSystemInfo", "13.1");

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

        public async Task<Result<SystemStatus>> GetSystemStatus()
        {
            var result = await _apiService.RequestJsonRpcAsync<SystemStatus, Dictionary<string, object>>("GetSystemStatus", "13.4");

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


        public async Task<Result<bool>> RebootDevice()
        {
            var result = await _apiService.RequestJsonRpcAsync<Dictionary<string, object>, Dictionary<string, object>>("SetDeviceReboot", "13.5");

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