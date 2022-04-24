using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using LinkZoneSdk.Models.Ussd;

namespace LinkZoneSdk
{
    internal sealed partial class Sdk : IUssd
    {
        public async Task<Result> Send(string ussdCode, int ussdType, CancellationToken? cancellation = null)
        {
            var result = await _apiService
                .RequestJsonRpcAsync<UssdData, Dictionary<string, object>>("SendUSSD", "8.1", parameters =>
                {
                    parameters.Add("UssdContent", ussdCode);
                    parameters.Add("UssdType", ussdType);
                }, null, cancellation)
                .ConfigureAwait(false);

            if (result.IsFailed)
            {
                return result.ToResult();
            }

            var resultValue = result.Value;

            if (RequestJsonRpcIsOk(resultValue))
            {
                return Result.Ok();
            }

            return Result.Fail("Failed to send USSD code");
        }

        public async Task<Result<UssdResultData>> GetSendResult(CancellationToken? cancellation = null)
        {
            var result = await _apiService
                .RequestJsonRpcAsync<UssdResultData, Dictionary<string, object>>("GetUSSDSendResult", "8.2", null, cancellation)
                .ConfigureAwait(false);
            
            if (result.IsFailed)
            {
                return result.ToResult();
            }

            var resultValue = result.Value;

            if (RequestJsonRpcIsOk(resultValue))
            {
                return Result.Ok(resultValue.ResultParameters);
            }

            return Result.Fail("Failed to retrieve USSD result");
        }

        public async Task<Result<bool>> SetEnd(CancellationToken? cancellation = null)
        {
            var result = await _apiService
                .RequestJsonRpcAsync<Dictionary<string, object>, Dictionary<string, object>>("SetUSSDEnd", "8.3", null, cancellation)
                .ConfigureAwait(false);

            if (result.IsFailed)
            {
                return result.ToResult();
            }

            return RequestJsonRpcIsOk(result.Value) ? Result.Ok(true) : Result.Fail("Failed to cancel USSD");
        }
    }
}