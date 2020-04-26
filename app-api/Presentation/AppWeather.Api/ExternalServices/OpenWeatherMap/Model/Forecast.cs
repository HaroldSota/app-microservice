namespace AppWeather.Api.ExternalServices.OpenWeatherMap.Model
{
    public class Forecast
    {
        public List[] list { get; set; }
        public City city { get; set; }
    }
}
