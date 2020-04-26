using System.Text.Json;

namespace AppWeather.Tests.Infrastructure
{
    public static class ClientSerializer
    {
        private static readonly JsonSerializerOptions SerializerOptions;
        static ClientSerializer()
        {
            SerializerOptions = new JsonSerializerOptions
            {
                IgnoreNullValues = true
            };
        }

        public static T Deserialize<T>(string jsonString)
        {
            return JsonSerializer.Deserialize<T>(jsonString, SerializerOptions);
        }

        public static string Serialize<T>(T @object)
        {
            return JsonSerializer.Serialize(@object, SerializerOptions);
        }
    }
}
