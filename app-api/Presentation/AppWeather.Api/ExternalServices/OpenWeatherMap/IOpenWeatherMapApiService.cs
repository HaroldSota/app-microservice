using AppWeather.Api.ExternalServices.OpenWeatherMap.Model;
using System.Threading.Tasks;

namespace AppWeather.Api.ExternalServices.OpenWeatherMap
{
    public interface IOpenWeatherMapApiService
    {
        /// <summary>
        /// Get forecast by city name
        /// </summary>
        /// <param name="cityName">The city name to</param>
        /// <returns></returns>
        Task<ApiResponse<Forecast, Error>> GetForecastByCityName(string cityName);

        /// <summary>
        /// Get forecast by zip code
        /// </summary>
        /// <param name="zipCode">The zipcode</param>
        /// <returns></returns>
        Task<ApiResponse<Forecast, Error>> GetForecastByZipCode(string zipCode);

        /// <summary>
        /// Get forecast by coordinate
        /// </summary>
        /// <param name="lat">The latitude</param>
        /// <param name="lon">The longitude</param>
        /// <returns></returns>
        Task<ApiResponse<Forecast, Error>> GetForecastByCoordinate(float lat, float lon);
    }
}