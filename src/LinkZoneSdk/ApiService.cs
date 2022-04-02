using FluentResults;
using LinkZoneSdk.Errors;
using LinkZoneSdk.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading;
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

        public Task<Result<ResultData<TResult, TError>>> RequestJsonRpcAsync<TResult, TError>(string method, string id,
            CancellationToken? cancellation = null)
            where TError : class
            where TResult : class
        {
            return RequestJsonRpcAsync<TResult, TError>(method, id, parameters => { }, null, cancellation);
        }

        public Task<Result<ResultData<TResult, TError>>> RequestJsonRpcAsync<TResult, TError>(string method, string id, 
            IPAddress? endpoint = null, CancellationToken? cancellation = null)
            where TResult : class where TError : class
        {
            return RequestJsonRpcAsync<TResult, TError>(method, id, parameters => { }, endpoint, cancellation);
        }

        public async Task<Result<ResultData<TResult, TError>>> RequestJsonRpcAsync<TResult, TError>(string method,
            string id, Action<Dictionary<string, object>> parametersBuilder, IPAddress? endpoint = null,
            CancellationToken? cancellation = null)
            where TResult : class
            where TError : class
        {
            var client = _httpClientFactory.CreateClient();

            var postData = BuildPostData(method, id, parametersBuilder);

            return await OtherExecuteApiPostCall<TResult, TError>(method, client, postData, endpoint, cancellation).ConfigureAwait(false);
        }

        private static async Task<Result<ResultData<TResult, TError>>> OtherExecuteApiPostCall<TResult, TError>(string method, HttpClient client,
            PostData postData, IPAddress? address, CancellationToken? cancellation)
            where TError : class
            where TResult : class
        {
            var url = address == null
                ? FullApiAddress + method
                : BuildUrl(address) + ApiCall + method;

            var myContent = JsonSerializer.Serialize(postData);
            var buffer = Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            
            var postResponseResult = await Result
                .Try(async () => await client.PostAsync(url, byteContent, cancellation ?? CancellationToken.None),
                    ExceptionCatchHandler())
                .ConfigureAwait(false);

            if (postResponseResult.IsFailed)
            {
                return postResponseResult.ToResult();
            }

            var postResponse = postResponseResult.Value;

            postResponse.EnsureSuccessStatusCode();

            var result = await postResponse.Content.ReadFromJsonAsync<ResultData<TResult, TError>>().ConfigureAwait(false);

            if (result != null) return Result.Ok(result);

            var dataReceived = await postResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            return Result.Fail<ResultData<TResult, TError>>(new InvalidJsonDataReceivedError(dataReceived));
        }

        private static Func<Exception, IError> ExceptionCatchHandler()
        {
            return ex =>
            {
                switch (ex)
                {
                    case HttpRequestException httpRequestException:
                        return new HttpRequestError().CausedBy(httpRequestException);
                    case TaskCanceledException taskCanceledException:
                        return new Error(ex.Message).CausedBy(taskCanceledException);
                    case OperationCanceledException operationCanceledException:
                        return new Error(ex.Message).CausedBy(operationCanceledException);
                }

                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                }

                throw ex;
            };
        }

        private static PostData BuildPostData(string method, string id, Action<Dictionary<string, object>> parametersBuilder)
        {
            var pairs = new Dictionary<string, object>();

            parametersBuilder(pairs);

            return new PostData(method, id, pairs);
        }
    }
}
