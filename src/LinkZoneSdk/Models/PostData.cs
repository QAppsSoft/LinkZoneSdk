using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LinkZoneSdk.Models
{
    public class PostData
    {
        public PostData(string method, string id, Dictionary<string, object>? parameters = null) :
            this("2.0", method, id, parameters)
        {
        }

        public PostData(string jsonRpc, string method, string id, Dictionary<string, object>? parameters = null)
        {
            JsonRpc = jsonRpc;
            Method = method;
            Id = id;
            Params = parameters == null || parameters.Count == 0 ? null : parameters;
        }

        [JsonPropertyName("jsonrpc")]
        public string JsonRpc { get; }

        [JsonPropertyName("method")]
        public string Method { get; }

        [JsonPropertyName("params")]
        public Dictionary<string, object>? Params { get; }

        [JsonPropertyName("id")]
        public string Id { get; }
    }
}