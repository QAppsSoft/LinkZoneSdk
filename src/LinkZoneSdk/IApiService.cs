using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentResults;
using LinkZoneSdk.Models;

namespace LinkZoneSdk
{
    public interface IApiService
    {
        Task<Result<ResultData<TResult, TError>>> RequestJsonRpcAsync<TResult, TError>(string method, string id)
            where TError : class
            where TResult : class;

        Task<Result<ResultData<TResult, TError>>> RequestJsonRpcAsync<TResult, TError>(string method, string id, Action<Dictionary<string, string>> parametersBuilder, IPAddress? endpoint = null)
            where TResult : class
            where TError : class;
    }
}