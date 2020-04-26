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
    public sealed class GetForecastByCityNameHandler : BaseHandler, IRequestHandler<GetForecastByCityNameRequest, QueryResponse<GetForecastByCityNameResponse>>
    {
        private readonly IOpenWeatherMapApiService _openWeatherMapApiService;
        private readonly IUserSearchRepository _userSearchRepository;
        private readonly IMemoryCache _cache;
        private List<string> Errors { get; set; } = new List<string>();


        public GetForecastByCityNameHandler(IOpenWeatherMapApiService openWeatherMapApiService, IUserSearchRepository userSearchRepository, IMemoryCache cache)
        {
            _openWeatherMapApiService = openWeatherMapApiService;
            _userSearchRepository = userSearchRepository;
            _cache = cache;
        }

        /// <summary>
        ///     Handle GetForecastByCityNameRequest request
        /// </summary>
        /// <param name="query">query</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<QueryResponse<GetForecastByCityNameResponse>> Handle(GetForecastByCityNameRequest query, CancellationToken cancellationToken)
        {
            try
            {
                if (!Validate(query))
                    return new QueryResponse<GetForecastByCityNameResponse>(MessageType.Validation, new QueryResponseError("Validate", Errors.First()));

                var apiResult = await ExecuteQuery(query);

                if (apiResult.IsSuccessful)
                {
                    var current = apiResult.Response.list.ToList().OrderBy(item => item.dt_txt).First();

                    _userSearchRepository.Add(new UserSearch(query.UserId, apiResult.Response.city.name, current.main.temp, current.main.humidity, DateTime.Now));

                    return new QueryResponse<GetForecastByCityNameResponse>(
                        new GetForecastByCityNameResponse
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
                    return new QueryResponse<GetForecastByCityNameResponse>(msgType, new QueryResponseError(apiResult.Error.cod, apiResult.Error.message));
                }
            }
            catch (Exception ex)
            {
                return new QueryResponse<GetForecastByCityNameResponse>(MessageType.Error, ex);
            }
        }

        private bool Validate(GetForecastByCityNameRequest query)
        {
            if (query == null)
                this.Errors.Add("Error: Query parameter is null!");
            else
            {
                if (string.IsNullOrEmpty(query.CityName))
                    this.Errors.Add("Error: CityName is empty!");
                else if (query.CityName.Length < 3)
                    this.Errors.Add("Error: CityName must be at least 3 alphabetic letters!");
                else if (Regex.IsMatch(query.CityName, "[^a-zA-ZöäüÖÄÜß :]"))
                    this.Errors.Add("The CityName can contain only german alphabetic letters and space!");
            }

            return Errors.Count == 0;
        }
        private async Task<ApiResponse<Forecast, Error>> ExecuteQuery(GetForecastByCityNameRequest query)
        {
            string cacheKey = GetCacheKey(query);
            return await _cache.GetOrCreateAsync(cacheKey, (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(3);
                entry.SlidingExpiration = TimeSpan.FromHours(3);
                return _openWeatherMapApiService.GetForecastByCityName(query.CityName);
            });
        }
    }
}