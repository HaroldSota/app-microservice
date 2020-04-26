using AppWeather.Api.ExternalServices.OpenWeatherMap.Model;
using System.Linq;
using System.Threading.Tasks;

namespace AppWeather.Api.ExternalServices.OpenWeatherMap
{
    public sealed class OpenWeatherMapApiService : RestClient, IOpenWeatherMapApiService
    {
        public OpenWeatherMapApiService(IOpenWeatherMapConfiguration bindingConfiguration)
            : base(bindingConfiguration)
        {
        }

        public async Task<ApiResponse<Forecast, Error>> GetForecastByCityName(string cityName)
        {
            var resourceConfiguration = BindingConfiguration.Resources.FirstOrDefault(r => r.Name.Equals("GetForecastByCityName"));

            return
                await ConfigureClient(resourceConfiguration)
                           .GetAsync<Forecast, Error>(string.Format($"{this.BindingConfiguration.Endpoint}{resourceConfiguration.Location}&{BindingConfiguration.ApiKeyName}={BindingConfiguration.ApiKeyValue}", cityName));
        }

        public async Task<ApiResponse<Forecast, Error>> GetForecastByZipCode(string zipCode)
        {
            var resourceConfiguration = BindingConfiguration.Resources.FirstOrDefault(r => r.Name.Equals("GetForecastByZipCode"));

            return
                await ConfigureClient(resourceConfiguration)
                           .GetAsync<Forecast, Error>(string.Format($"{this.BindingConfiguration.Endpoint}{resourceConfiguration.Location}&{BindingConfiguration.ApiKeyName}={BindingConfiguration.ApiKeyValue}", zipCode));
        }

        public async Task<ApiResponse<Forecast, Error>> GetForecastByCoordinate(float lat, float lon)
        {
            var resourceConfiguration = BindingConfiguration.Resources.FirstOrDefault(r => r.Name.Equals("GetForecastByCoordinate"));

            return
                await ConfigureClient(resourceConfiguration)
                           .GetAsync<Forecast, Error>(string.Format($"{this.BindingConfiguration.Endpoint}{resourceConfiguration.Location}&{BindingConfiguration.ApiKeyName}={BindingConfiguration.ApiKeyValue}", lat, lon));
        }
    }
}