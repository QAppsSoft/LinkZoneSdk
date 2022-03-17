using System.Text.Json.Serialization;

namespace LinkZoneSdk.Models
{
    public sealed class ResultData<TResult, TError>
        where TResult : class
        where TError : class
    {
        public ResultData(string jsonRpc, TResult? resultParameters, TError? errorParameters, string id)
        {
            JsonRpc = jsonRpc;
            ResultParameters = resultParameters;
            ErrorParameters = errorParameters;
            Id = id;
        }

        [JsonPropertyName("jsonrpc")]
        public string JsonRpc { get; }

        [JsonPropertyName("result")]
        public TResult? ResultParameters { get; }

        [JsonPropertyName("error")]
        public TError? ErrorParameters { get; }

        [JsonPropertyName("id")]
        public string Id { get; }
    }
}
