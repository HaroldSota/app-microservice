namespace AppWeather.Api.ExternalServices
{
    /// <summary>
    ///     External api wrapper response 
    /// </summary>
    /// <typeparam name="TResource">Expected return data type in OK status response</typeparam>
    /// <typeparam name="TError">The Error type in case of failure of the request</typeparam>
    public class ApiResponse<TResource, TError>
    {
        /// <summary>
        ///  Boolean indicating if the rest call was successful or not
        /// </summary>
        public bool IsSuccessful => Error == null;

        /// <summary>
        ///     The response object in case of successful rest call
        /// </summary>
        public TResource Response { get; set; }

        /// <summary>
        ///     The error object in case of unsuccessful rest call
        /// </summary>
        public TError Error { get; set; }
    }
}
