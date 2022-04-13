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
    internal sealed class ApiService : IApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private const string ApiCall = "/jrd/webapi?api=";

        private static readonly Func<IPAddress, string> BuildUrl = address => $"http://{address}";

        private static IPAddress IpAddress => Sdk.Address;

        public static readonly string ServicePath = BuildUrl(IpAddress);
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
            ApiSettings? apiSettings = null, CancellationToken? cancellation = null)
            where TResult : class where TError : class
        {
            return RequestJsonRpcAsync<TResult, TError>(method, id, parameters => { }, apiSettings, cancellation);
        }

        public Task<Result<ResultData<TResult, TError>>> RequestJsonRpcAsync<TResult, TError>(string method,
            string id, Action<Dictionary<string, object>> parametersBuilder, ApiSettings? apiSettings = null,
            CancellationToken? cancellation = null)
            where TResult : class
            where TError : class
        {
            var client = _httpClientFactory.CreateClient();

            var postData = BuildPostData(method, id, parametersBuilder);

            return ExecuteApiPostCall<TResult, TError>(method, client, postData, apiSettings, cancellation);
        }

        private static async Task<Result<ResultData<TResult, TError>>> ExecuteApiPostCall<TResult, TError>(string method, HttpClient client,
            PostData postData, ApiSettings? apiSettings, CancellationToken? cancellation)
            where TError : class
            where TResult : class
        {
            var timeout = apiSettings?.Timeout ?? ApiSettings.Default().Timeout;

            using var tokenSourceWithTimeout = cancellation.HasValue
                ? CancellationTokenSource.CreateLinkedTokenSource(cancellation.Value)
                : new CancellationTokenSource();

            tokenSourceWithTimeout.CancelAfter(timeout);

            var url = apiSettings.HasValue
                ? BuildUrl(apiSettings.Value.Address) + ApiCall + method
                : FullApiAddress + method;

            var myContent = JsonSerializer.Serialize(postData);
            var buffer = Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            
            var postResponseResult = await Result
                .Try(() => client.PostAsync(url, byteContent, tokenSourceWithTimeout.Token),
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
