using MediatR;
using System.Text.Json.Serialization;

namespace AppWeather.Api.Messaging.Model.SearchHistory
{
    public class GetAllRequest : IRequest<QueryResponse<GetAllResponse[]>>
    {
        [JsonPropertyName("UserId")]
        public string UserId { get; set; }
    }

    public class GetAllResponse
    {
        [JsonPropertyName("CityName")]
        public string CityName { get; set; }

        [JsonPropertyName("Temperature")]
        public float? Temperature { get; set; }

        [JsonPropertyName("Humidity")]
        public int? Humidity { get; set; }
    }
}
