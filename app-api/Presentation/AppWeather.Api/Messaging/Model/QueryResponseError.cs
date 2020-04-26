using System;
using System.Text.Json.Serialization;

namespace AppWeather.Api.Messaging.Model
{
    public sealed class QueryResponseError
    {

        public QueryResponseError()
        {
            //Only for testing purpose
        }


        public QueryResponseError(string errorType, string message)
        {
            ErrorType = errorType;
            Message = message;
        }

        public QueryResponseError(Exception exception)
        {
            ErrorType = exception.GetType().Name;
            Message = exception.Message;
            Obj = exception;
        }

        [JsonPropertyName("ErrorType")]
        public string ErrorType { get; set; }

        [JsonPropertyName("Message")]
        public string Message { get; set; }

        [JsonPropertyName("Obj")]
        public Exception Obj { get; set; }
    }
}
