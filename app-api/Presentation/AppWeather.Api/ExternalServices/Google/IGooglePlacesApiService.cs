using AppWeather.Api.ExternalServices.Google.Model;
using System.Threading.Tasks;

namespace AppWeather.Api.ExternalServices.Google
{
    public interface IGooglePlacesApiService
    {
        Task<ApiResponse<Places, Error>> GetByPlacesSuggestionAsync(string place);
    }
}