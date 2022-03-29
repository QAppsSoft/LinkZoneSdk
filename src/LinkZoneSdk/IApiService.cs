using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using LinkZoneSdk.Models;

namespace LinkZoneSdk
{
    public interface IApiService
    {
        Task<Result<ResultData<TResult, TError>>> RequestJsonRpcAsync<TResult, TError>(string method, string id, IPAddress? endpoint = null, CancellationToken? cancellation = null)
            where TResult : class
            where TError : class;

        Task<Result<ResultData<TResult, TError>>> RequestJsonRpcAsync<TResult, TError>(string method, string id, Action<Dictionary<string, string>> parametersBuilder, IPAddress? endpoint = null, CancellationToken? cancellation = null)
            where TResult : class
            where TError : class;
    }
}