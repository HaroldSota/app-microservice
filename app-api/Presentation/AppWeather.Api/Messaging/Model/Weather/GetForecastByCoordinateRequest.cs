using MediatR;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AppWeather.Api.Messaging.Model.Weather
{
    public sealed class GetForecastByCoordinateRequest : IRequest<QueryResponse<GetForecastByCoordinateResponse>>
    {
        [JsonPropertyName("UserId")]
        public string UserId { get; set; }

        [JsonPropertyName("Coordinate")]
        public string Coordinate { get; set; }
    }

    public sealed class GetForecastByCoordinateResponse
    {
        [JsonPropertyName("Forecasts")]
        public List<DayForecast> Forecasts { get; set; } = new List<DayForecast>();

        [JsonPropertyName("Locality")]
        public Locality Locality { get; set; }
    }
}
