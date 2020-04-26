using System.Text.Json.Serialization;

namespace AppWeather.Api.Messaging.Model.Weather
{
    public class DayForecast
    {
        [JsonPropertyName("Day")]
        public string Day { get; set; }

        [JsonPropertyName("Date")]
        public string Date { get; set; }

        [JsonPropertyName("MinTemp")]
        public int MinTemp { get; set; } = 0;

        [JsonPropertyName("MaxTemp")]
        public int MaxTemp { get; set; } = 0;

        [JsonPropertyName("AvgTemp")]
        public int AvgTemp => (int)(MinTemp + MaxTemp) / 2;

        [JsonPropertyName("AvgHumidity")]
        public int AvgHumidity { get; set; }

        [JsonPropertyName("AvgWindSpeed")]
        public int AvgWindSpeed { get; set; }
    }
}
