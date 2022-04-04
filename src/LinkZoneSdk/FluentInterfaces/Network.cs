using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using LinkZoneSdk.Enums;
using LinkZoneSdk.Models.Network;

namespace LinkZoneSdk
{
    internal sealed partial class Sdk : INetwork
    {
        public async Task<Result<NetworkSettings>> GetSettings(CancellationToken? cancellation = null)
        {
            var result = await _apiService
                .RequestJsonRpcAsync<NetworkSettings, Dictionary<string, object>>("GetNetworkSettings", "4.6", null,
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

            return Result.Fail("Failed to retrieve network settings");
        }

        public async Task<Result<bool>> SetSettings(NetworkMode networkMode, NetworkSelection networkSelectionMode, CancellationToken? cancellation = null)
        {
            var result = await _apiService.RequestJsonRpcAsync<Dictionary<string, object>, Dictionary<string, object>>(
                "SetNetworkSettings", "4.7", parameters =>
                {
                    parameters.Add("NetworkMode", networkMode);
                    parameters.Add("NetselectionMode", networkSelectionMode);
                }, null, cancellation).ConfigureAwait(false);

            if (result.IsFailed)
            {
                return result.ToResult();
            }

            return RequestJsonRpcIsOk(result.Value) ? Result.Ok(true) : Result.Fail("Failed to set network settings");
        }
    }
}