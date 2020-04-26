using MediatR;
using System.Text.Json.Serialization;

namespace AppWeather.Api.Messaging.Model.SearchHistory
{
    public class GetLastRequest : IRequest<QueryResponse<GetLastResponse[]>>
    {
        [JsonPropertyName("UserId")]
        public string UserId { get; set; }

        [JsonPropertyName("UserId")]
        public int Count { get; set; }
    }

    public class GetLastResponse
    {
        [JsonPropertyName("CityName")]
        public string CityName { get; set; }

        [JsonPropertyName("Temperature")]
        public float? Temperature { get; set; }

        [JsonPropertyName("Humidity")]
        public int? Humidity { get; set; }
    }
}
