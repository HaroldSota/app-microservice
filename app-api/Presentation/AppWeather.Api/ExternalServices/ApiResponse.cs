namespace AppWeather.Api.ExternalServices
{
    /// <summary>
    ///     External api wrapper response 
    /// </summary>
    /// <typeparam name="TResource">Expected return data type in OK status response</typeparam>
    /// <typeparam name="TError">The Error type in case of failure of the request</typeparam>
    public class ApiResponse<TResource, TError>
    {
        public bool IsSuccessful => Error == null;

        public TResource Response { get; set; }

        public TError Error { get; set; }
    }
}
