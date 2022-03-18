using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LinkZoneSdk.JsonConverters
{
    public class NetworkTypeJsonConverter : JsonConverter<string>
    {
        public override string Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            var value = reader.GetInt32();

            return value switch
            {
                0 => "No service",
                1 => "2G",
                2 => "2G",
                3 => "3G",
                4 => "3G",
                5 => "3G",
                6 => "3G+",
                7 => "3G+",
                8 => "4G",
                9 => "4G+",
                _ => "Unknown network type"
            };
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
