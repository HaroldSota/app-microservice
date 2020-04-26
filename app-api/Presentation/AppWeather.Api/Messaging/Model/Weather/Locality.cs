using System.Text.Json.Serialization;

namespace AppWeather.Api.Messaging.Model.Weather
{
    public class Locality
    {
        [JsonPropertyName("CityName")]
        public string CityName { get; set; }

        [JsonPropertyName("Lat")]
        public double Lat { get; set; }

        [JsonPropertyName("Lon")]
        public double Lon { get; set; }
    }
}
