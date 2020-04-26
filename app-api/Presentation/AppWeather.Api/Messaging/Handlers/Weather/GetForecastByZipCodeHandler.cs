using AppWeather.Api.Messaging.Model;
using AppWeather.Api.Messaging.Model.Weather;
using AppWeather.Api.Domain.UserSearchModel;
using AppWeather.Api.ExternalServices.OpenWeatherMap;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using AppWeather.Api.ExternalServices;
using AppWeather.Api.ExternalServices.OpenWeatherMap.Model;

namespace AppWeather.Api.Messaging.Handlers.Weather
{
    public sealed class GetForecastByZipCodeHandler : BaseHandler, IRequestHandler<GetForecastByZipCodeRequest, QueryResponse<GetForecastByZipCodeResponse>>
    {

        private readonly IOpenWeatherMapApiService _openWeatherMapApiService;
        private readonly IUserSearchRepository _userSearchRepository;
        private readonly IMemoryCache _cache;

        public GetForecastByZipCodeHandler(IOpenWeatherMapApiService openWeatherMapApiService, IUserSearchRepository userSearchRepository, IMemoryCache cache)
        {
            _openWeatherMapApiService = openWeatherMapApiService;
            _userSearchRepository = userSearchRepository;
            _cache = cache;
        }

        public List<string> Errors { get; set; } = new List<string>();


        public async Task<QueryResponse<GetForecastByZipCodeResponse>> Handle(GetForecastByZipCodeRequest query, CancellationToken cancellationToken)
        {
            try
            {
                if (!Validate(query))
                {
                    return new QueryResponse<GetForecastByZipCodeResponse>(MessageType.Validation, new QueryResponseError("Validate", Errors.First()));
                }

                var apiResult = await ExecuteQuery(query);

                if (apiResult.IsSuccessful)
                {
                    var current = apiResult.Response.list.ToList().OrderBy(item => item.dt_txt).First();
                    _userSearchRepository.Add(new UserSearch(query.UserId, apiResult.Response.city.name, current.main.temp, current.main.humidity, DateTime.Now));

                    return new QueryResponse<GetForecastByZipCodeResponse>(
                        new GetForecastByZipCodeResponse
                        {
                            Locality = new Locality
                            {
                                CityName = apiResult.Response.city.name,
                                Lat = apiResult.Response.city.coord.lat,
                                Lon = apiResult.Response.city.coord.lon
                            },
                            Forecasts = apiResult.Response.list.ToList()
                                    .GroupBy(item => item.dt_txt.Substring(8, 2))
                                    .Select(group => new DayForecast()
                                    {
                                        Date = group.Key,
                                        Day = DateTime.Parse(group.First().dt_txt).DayOfWeek.ToString(),
                                        MinTemp = (int)group.Min(item => item.main.temp_min),
                                        MaxTemp = (int)group.Min(item => item.main.temp_max),
                                        AvgHumidity = (int)group.Average(item => item.main.humidity),
                                        AvgWindSpeed = (int)group.Average(item => item.wind.deg),
                                    }).ToList()
                        });
                }
                else
                {
                    var msgType = apiResult.Error.cod.Equals("404") ? MessageType.NotFound : MessageType.Error;
                    return new QueryResponse<GetForecastByZipCodeResponse>(msgType, new QueryResponseError(apiResult.Error.cod, apiResult.Error.message));
                }
            }
            catch (Exception ex)
            {
                return new QueryResponse<GetForecastByZipCodeResponse>(MessageType.Error, ex);
            }
        }

        private bool Validate(GetForecastByZipCodeRequest query)
        {
            if (query == null)
                this.Errors.Add("Error: Query parameter is null!");
            if (string.IsNullOrEmpty(query.ZipCode))
                this.Errors.Add("Error:Zip Code is empty!");
            else if (!Regex.IsMatch(query.ZipCode, "^(?!01000|99999)(0[1-9]\\d{3}|[1-9]\\d{4})$"))
                this.Errors.Add($"{query.ZipCode} is not a valid German zip code! Germany has 5 digits always from 01001 up to 99998.");

            return Errors.Count == 0;
        }


        private async Task<ApiResponse<Forecast, Error>> ExecuteQuery(GetForecastByZipCodeRequest query)
        {
            string cacheKey = GetCacheKey(query);
            return await _cache.GetOrCreateAsync(cacheKey, (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(3);
                entry.SlidingExpiration = TimeSpan.FromHours(3);

                return _openWeatherMapApiService.GetForecastByZipCode(query.ZipCode);
            });
        }
    }
}