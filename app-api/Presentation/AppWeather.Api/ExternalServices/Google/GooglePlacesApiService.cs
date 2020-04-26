using AppWeather.Api.ExternalServices.Google.Model;
using System.Linq;
using System.Threading.Tasks;

namespace AppWeather.Api.ExternalServices.Google
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class GooglePlacesApiService : RestClient, IGooglePlacesApiService
    {
        public GooglePlacesApiService(IGooglePlacesApiConfiguration bindingConfiguration)
            : base(bindingConfiguration)
        {
        }

        public async Task<ApiResponse<Places, Error>> GetByPlacesSuggestionAsync(string place)
        {
            var resourceConfiguration = BindingConfiguration.Resources.FirstOrDefault(r => r.Name.Equals("GetByLocation"));
            return
                await ConfigureClient(resourceConfiguration)
                           .GetAsync<Places, Error>(string.Format($"{this.BindingConfiguration.Endpoint}{resourceConfiguration.Location}&{BindingConfiguration.ApiKeyName}={BindingConfiguration.ApiKeyValue}", place));
        }
    }
}