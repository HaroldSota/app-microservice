using MediatR;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AppWeather.Api.Messaging.Model.Places
{
    public sealed class GetSuggestionRequest : IRequest<QueryResponse<GetSuggestionResponse>>
    {
        [JsonPropertyName("CityName")]
        public string CityName { get; set; }
    }

    public sealed class GetSuggestionResponse
    {
        [JsonPropertyName("Suggestions")]
        public List<string> SuggestionList { get; set; } = new List<string>();
    }
}