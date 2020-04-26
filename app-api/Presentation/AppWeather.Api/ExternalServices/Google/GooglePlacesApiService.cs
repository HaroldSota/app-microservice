using AppWeather.Api.ExternalServices.Google.Model;
using System.Linq;
using System.Threading.Tasks;

namespace AppWeather.Api.ExternalServices.Google
{
    /// <inheritdoc cref="IGooglePlacesApiService" />
    public sealed class GooglePlacesApiService : RestClient, IGooglePlacesApiService
    {
        /// <summary>
        ///     GooglePlacesApiService ctor.
        /// </summary>
        /// <param name="bindingConfiguration"></param>
        public GooglePlacesApiService(IGooglePlacesApiConfiguration bindingConfiguration)
            : base(bindingConfiguration)
        {
        }

        /// <inheritdoc />
        public async Task<ApiResponse<Places, Error>> GetByPlacesSuggestionAsync(string place)
        {
            var resourceConfiguration = BindingConfiguration.Resources.FirstOrDefault(r => r.Name.Equals("GetByLocation"));
            if (resourceConfiguration != null)
            {
                return
                    await ConfigureClient(resourceConfiguration)
                        .GetAsync<Places, Error>(string.Format(
                            $"{this.BindingConfiguration.Endpoint}{resourceConfiguration.Location}&{BindingConfiguration.ApiKeyName}={BindingConfiguration.ApiKeyValue}",
                            place));
            }
            return  new ApiResponse<Places, Error>()
            {
                Error = new Error(){ error_message = $"Resource definition 'GetByLocation' not found for {this.BindingConfiguration.Name}" }
            };
        }
    }
}