using AppWeather.Api.ExternalServices.OpenWeatherMap.Model;
using System.Linq;
using System.Threading.Tasks;

namespace AppWeather.Api.ExternalServices.OpenWeatherMap
{
    /// <inheritdoc cref="IOpenWeatherMapApiService" />
    public sealed class OpenWeatherMapApiService : RestClient, IOpenWeatherMapApiService
    {
        /// <summary>
        ///     OpenWeatherMapApiService ctor
        /// </summary>
        public OpenWeatherMapApiService(IOpenWeatherMapConfiguration bindingConfiguration)
            : base(bindingConfiguration)
        {
        }

        /// <inheritdoc />
        public async Task<ApiResponse<Forecast, Error>> GetForecastByCityName(string cityName)
        {
            var resourceConfiguration = BindingConfiguration.Resources.FirstOrDefault(r => r.Name.Equals("GetForecastByCityName"));

            if (resourceConfiguration != null)
            {
                return
                    await ConfigureClient(resourceConfiguration)
                        .GetAsync<Forecast, Error>(string.Format(
                            $"{this.BindingConfiguration.Endpoint}{resourceConfiguration.Location}&{BindingConfiguration.ApiKeyName}={BindingConfiguration.ApiKeyValue}",
                            cityName));
            }

            return new ApiResponse<Forecast, Error>()
            {
                Error = new Error() { message = $"Resource definition 'GetForecastByCityName' not found for {this.BindingConfiguration.Name}" }
            };
        }

        /// <inheritdoc />
        public async Task<ApiResponse<Forecast, Error>> GetForecastByZipCode(string zipCode)
        {
            var resourceConfiguration = BindingConfiguration.Resources.FirstOrDefault(r => r.Name.Equals("GetForecastByZipCode"));

            if (resourceConfiguration != null)
            {
                return
                    await ConfigureClient(resourceConfiguration)
                        .GetAsync<Forecast, Error>(string.Format(
                            $"{this.BindingConfiguration.Endpoint}{resourceConfiguration.Location}&{BindingConfiguration.ApiKeyName}={BindingConfiguration.ApiKeyValue}",
                            zipCode));
            }

            return new ApiResponse<Forecast, Error>()
            {
                Error = new Error() {message = $"Resource definition 'GetForecastByZipCode' not found for {this.BindingConfiguration.Name}" }
            };
        }

        /// <inheritdoc />
        public async Task<ApiResponse<Forecast, Error>> GetForecastByCoordinate(float lat, float lon)
        {
            var resourceConfiguration = BindingConfiguration.Resources.FirstOrDefault(r => r.Name.Equals("GetForecastByCoordinate"));

            if (resourceConfiguration != null)
            {
                return
                    await ConfigureClient(resourceConfiguration)
                        .GetAsync<Forecast, Error>(string.Format(
                            $"{this.BindingConfiguration.Endpoint}{resourceConfiguration.Location}&{BindingConfiguration.ApiKeyName}={BindingConfiguration.ApiKeyValue}",
                            lat, lon));
            }
            return new ApiResponse<Forecast, Error>()
            {
                Error = new Error() { message = $"Resource definition 'GetForecastByCoordinate' not found for {this.BindingConfiguration.Name}" }
            };
        }
    }
}