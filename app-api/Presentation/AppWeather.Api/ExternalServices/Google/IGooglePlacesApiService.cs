using AppWeather.Api.ExternalServices.Google.Model;
using System.Threading.Tasks;

namespace AppWeather.Api.ExternalServices.Google
{
    /// <summary>
    ///     GooglePlacesApiService implemented api actions 
    /// </summary>
    public interface IGooglePlacesApiService
    {
        /// <summary>
        ///     Get a list of suggestions from the given string
        /// </summary>
        /// <param name="place">Place query string</param>
        /// <returns></returns>
        Task<ApiResponse<Places, Error>> GetByPlacesSuggestionAsync(string place);
    }
}