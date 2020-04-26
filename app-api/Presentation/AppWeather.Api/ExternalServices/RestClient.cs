using AppWeather.Api.Framework.Configuration.Bindings;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AppWeather.Api.ExternalServices
{
    /// <summary>
    ///     Client to create Http requests and process response result
    /// </summary>
    public abstract class RestClient
    {
        /// <summary>
        ///     The client binding configuration 
        /// </summary>
        protected readonly IBindingConfiguration BindingConfiguration;

        private readonly HttpClient _client =  new HttpClient(new HttpClientHandler());
       
        private readonly int _size = 1000;

        /// <summary>
        ///     Sets the api binging properties for requests made by this client instance
        /// </summary>
        /// <param name="bindingConfiguration"></param>
        protected RestClient(IBindingConfiguration bindingConfiguration)
        {
            BindingConfiguration = bindingConfiguration;
        }

        /// <summary>
        ///     Configures the HttpClient with the resource configuration info
        /// </summary>
        /// <param name="resourceConfiguration"></param>
        /// <returns></returns>
        protected RestClient ConfigureClient(IBindingResourceConfiguration resourceConfiguration)
        {
            if (string.IsNullOrEmpty(BindingConfiguration.Endpoint))
            {
                throw new Exception("BaseAddress/Endpoint is required!");
            }

            _client.BaseAddress = new Uri(BindingConfiguration.Endpoint);

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Range = new RangeHeaderValue(0, _size);
            _client.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse(resourceConfiguration.ContentType));
            _client.Timeout = resourceConfiguration.Timeout == 0 ? new TimeSpan(0, 0, 0, 10) : new TimeSpan(0, 0, 0, resourceConfiguration.Timeout);

            return this;
        }

        /// <summary>
        ///    Executes a GET-style request and callback asynchronously
        /// </summary>
        /// <typeparam name="TResource">Expected return data type in OK status response</typeparam>
        /// <typeparam name="TError">The Error type in case of failure of the request</typeparam>
        /// <param name="uri">Api action URI</param>
        /// <returns></returns>
        public async Task<ApiResponse<TResource, TError>> GetAsync<TResource, TError>(string uri)
        {
            return await InvokeAsync<TResource, TError>
                (
                       client => client.GetAsync(uri),
                       async (response, deserializeType) =>
                       {

                           // get result type object
                           if (deserializeType.Equals(typeof(TResource)))
                           {
                               if (!typeof(TResource).Equals(typeof(string)))
                                   return JsonConvert.DeserializeObject<TResource>(await response.Content.ReadAsStringAsync());

                               return await response.Content.ReadAsStringAsync();
                           }

                           // get error type object
                           return JsonConvert.DeserializeObject<TError>(await response.Content.ReadAsStringAsync());
                       }
                );
        }

        private async Task<ApiResponse<TResource, TError>> InvokeAsync<TResource, TError>(
            Func<HttpClient, Task<HttpResponseMessage>> apiCaller,
            Func<HttpResponseMessage, Type, Task<object>> responseReader = null)
        {
            if (apiCaller == null)
                throw new ArgumentNullException(nameof(apiCaller));

            HttpResponseMessage httpResponse;

            try
            {
                httpResponse = await apiCaller(_client).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new Exception($"Error communicating with {BindingConfiguration.Name}", e);
            }

            if (!httpResponse.IsSuccessStatusCode)
            {
                var exception = new Exception($"{BindingConfiguration.Name} returned an error. StatusCode : {httpResponse.StatusCode}");
                exception.Data.Add("StatusCode", httpResponse.StatusCode);
                try
                {
                    return new ApiResponse<TResource, TError> { Error = responseReader != null ? (TError)(await responseReader(httpResponse, typeof(TError)).ConfigureAwait(false)) : default };
                }
                catch
                {
                    throw new Exception($"{BindingConfiguration.Name} returned an error. StatusCode : {httpResponse.StatusCode}", exception);
                }
            }

            return new ApiResponse<TResource, TError> { Response = responseReader != null ? (TResource)(await responseReader(httpResponse, typeof(TResource)).ConfigureAwait(false)) : default };
        }
    }
}
