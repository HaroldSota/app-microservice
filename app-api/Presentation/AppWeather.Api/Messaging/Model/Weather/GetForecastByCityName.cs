using MediatR;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AppWeather.Api.Messaging.Model.Weather
{
    public sealed class GetForecastByCityNameRequest : IRequest<QueryResponse<GetForecastByCityNameResponse>>
    {
        [JsonPropertyName("UserId")]
        internal string UserId { get; set; }

        [JsonPropertyName("CityName")]
        public string CityName { get; set; }
    }

    public sealed class GetForecastByCityNameResponse
    {
        [JsonPropertyName("Forecasts")]
        public List<DayForecast> Forecasts { get; set; } = new List<DayForecast>();

        [JsonPropertyName("Locality")]
        public Locality Locality { get; set; }
    }
}