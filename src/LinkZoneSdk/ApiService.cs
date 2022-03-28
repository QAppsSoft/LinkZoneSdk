﻿using FluentResults;
using LinkZoneSdk.Errors;
using LinkZoneSdk.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LinkZoneSdk
{
    public sealed class ApiService : IApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private const string ApiCall = "/jrd/webapi?api=";

        private static readonly Func<IPAddress, string> BuildUrl = address => $"http://{address}";

        public static readonly string ServicePath = BuildUrl(Sdk.DefaultAddress);
        private static readonly string FullApiAddress = ServicePath + ApiCall;

        public ApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public Task<Result<ResultData<TResult, TError>>> RequestJsonRpcAsync<TResult, TError>(string method, string id)
            where TError : class
            where TResult : class
        {
            return RequestJsonRpcAsync<TResult, TError>(method, id, parameters => { });
        }

        public async Task<Result<ResultData<TResult, TError>>> RequestJsonRpcAsync<TResult, TError>(string method, string id, Action<Dictionary<string, string>> parametersBuilder, IPAddress? endpoint = null)
            where TResult : class
            where TError : class
        {
            var client = _httpClientFactory.CreateClient();

            var postData = BuildPostData(method, id, parametersBuilder);

            return await OtherExecuteApiPostCall<TResult, TError>(method, client, postData, endpoint).ConfigureAwait(false);
        }

        private static async Task<Result<ResultData<TResult, TError>>> OtherExecuteApiPostCall<TResult, TError>(string method, HttpClient client, PostData postData, IPAddress? address)
            where TError : class
            where TResult : class
        {
            var url = address == null
                ? FullApiAddress + method
                : BuildUrl(address) + ApiCall + method;

            var myContent = JsonSerializer.Serialize(postData);
            var buffer = Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);

            var postResponse = await client.PostAsync(url, byteContent).ConfigureAwait(false);

            postResponse.EnsureSuccessStatusCode();

            var result = await postResponse.Content.ReadFromJsonAsync<ResultData<TResult, TError>>().ConfigureAwait(false);

            if (result != null) return Result.Ok(result);

            var dataReceived = await postResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            return Result.Fail<ResultData<TResult, TError>>(new InvalidJsonDataReceivedError(dataReceived));
        }

        private static PostData BuildPostData(string method, string id, Action<Dictionary<string, string>> parametersBuilder)
        {
            var pairs = new Dictionary<string, string>();

            parametersBuilder(pairs);

            return new PostData(method, id, pairs);
        }
    }
}