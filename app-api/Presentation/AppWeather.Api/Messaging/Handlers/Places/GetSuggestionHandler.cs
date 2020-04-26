using AppWeather.Api.Messaging.Model;
using AppWeather.Api.Messaging.Model.Places;
using AppWeather.Api.ExternalServices.Google;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AppWeather.Api.Messaging.Handlers.Places
{
    public class GetSuggestionHandler : BaseHandler, IRequestHandler<GetSuggestionRequest, QueryResponse<GetSuggestionResponse>>
    {
        private readonly IGooglePlacesApiService _googlePlacesApiService;
        private readonly IMemoryCache _cache;
        private List<string> Errors { get; set; } = new List<string>();

        public GetSuggestionHandler(IGooglePlacesApiService googlePlacesApiService, IMemoryCache cache)
        {
            _googlePlacesApiService = googlePlacesApiService;
            _cache = cache;
        }

        public async Task<QueryResponse<GetSuggestionResponse>> Handle(GetSuggestionRequest query, CancellationToken cancellationToken)
        {
            try
            {
                if (!Validate(query))
                {
                    return new QueryResponse<GetSuggestionResponse>(MessageType.Validation, new QueryResponseError("Validate", Errors.First()));
                }

                string cacheKey = GetCacheKey(query);

                return await _cache.GetOrCreateAsync(cacheKey, (entry) =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(365);
                    entry.SlidingExpiration = TimeSpan.FromDays(365);

                    return ExecuteQuery(query);
                });

            }
            catch
            {
                // Default behavior no suggestion
                return new QueryResponse<GetSuggestionResponse>(new GetSuggestionResponse());
            }
        }

        private bool Validate(GetSuggestionRequest query)
        {
            if (query == null)
                Errors.Add("Error: Query parameter is null!");

            return Errors.Count == 0;
        }

        private async Task<QueryResponse<GetSuggestionResponse>> ExecuteQuery(GetSuggestionRequest query)
        {
            if (!string.IsNullOrEmpty(query.CityName))
            {
                var apiResult = await _googlePlacesApiService.GetByPlacesSuggestionAsync(query.CityName);

                if (apiResult.IsSuccessful)
                {
                    return new QueryResponse<GetSuggestionResponse>(new GetSuggestionResponse()
                    {
                        SuggestionList = apiResult.Response.predictions
                                                    .Select(p => p.structured_formatting.main_text)
                                                    .Distinct()
                                                    .ToList()
                    });
                }
            }

            // Default behavior no suggestion
            return new QueryResponse<GetSuggestionResponse>(new GetSuggestionResponse());
        }
    }
}