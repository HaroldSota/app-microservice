namespace AppWeather.Api.ExternalServices.Google.Model
{
    public class Places
    {
        public string status { get; set; }
        public Prediction[] predictions { get; set; }
    }
}
