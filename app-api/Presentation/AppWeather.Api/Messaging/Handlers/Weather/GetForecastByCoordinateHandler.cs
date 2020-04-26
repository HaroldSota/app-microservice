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
    public sealed class GetForecastByCoordinateHandler : BaseHandler, IRequestHandler<GetForecastByCoordinateRequest, QueryResponse<GetForecastByCoordinateResponse>>
    {
        private readonly IOpenWeatherMapApiService _openWeatherMapApiService;
        private readonly IUserSearchRepository _userSearchRepository;
        private readonly IMemoryCache _cache;

        public GetForecastByCoordinateHandler(IOpenWeatherMapApiService openWeatherMapApiService, IUserSearchRepository userSearchRepository, IMemoryCache cache)
        {
            _openWeatherMapApiService = openWeatherMapApiService;
            _userSearchRepository = userSearchRepository;
            _cache = cache;
        }

        public List<string> Errors { get; set; } = new List<string>();

        public async Task<QueryResponse<GetForecastByCoordinateResponse>> Handle(GetForecastByCoordinateRequest query, CancellationToken cancellationToken)
        {
            try
            {
                if (!Validate(query))
                {
                    return new QueryResponse<GetForecastByCoordinateResponse>(MessageType.Validation, new QueryResponseError("Validate", Errors.First()));
                }

                var apiResult = await ExecuteQuery(query);

                if (apiResult.IsSuccessful)
                {
                    var current = apiResult.Response.list.ToList().OrderBy(item => item.dt_txt).First();
                    _userSearchRepository.Add(new UserSearch(query.UserId, apiResult.Response.city.name, current.main.temp, current.main.humidity, DateTime.Now));

                    return new QueryResponse<GetForecastByCoordinateResponse>(
                        new GetForecastByCoordinateResponse
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
                    return new QueryResponse<GetForecastByCoordinateResponse>(MessageType.Validation, new QueryResponseError(apiResult.Error.cod, apiResult.Error.message));
                }
            }
            catch (Exception ex)
            {
                return new QueryResponse<GetForecastByCoordinateResponse>(MessageType.Validation, ex);
            }
        }

        private bool Validate(GetForecastByCoordinateRequest query)
        {
            if (query == null)
                this.Errors.Add("Error: Query parameter is null!");
            else
            {
                if (string.IsNullOrEmpty(query.Coordinate))
                    this.Errors.Add("Error: GeoCode is null or empty!");
                else if (!Regex.IsMatch(query.Coordinate, "^[-+]?[0-9]*\\.?[0-9]+,[-+]?[0-9]*\\.?[0-9]+$"))
                    this.Errors.Add("The given GeoCode is not in the correct format e.g. lat,lon!");
            }

            return Errors.Count == 0;
        }

        private async Task<ApiResponse<Forecast, Error>> ExecuteQuery(GetForecastByCoordinateRequest query)
        {
            string cacheKey = GetCacheKey(query);
            return await _cache.GetOrCreateAsync(cacheKey, (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(3);
                entry.SlidingExpiration = TimeSpan.FromHours(3);
                var lat = float.Parse(query.Coordinate.Split(',')[0]);
                var lon = float.Parse(query.Coordinate.Split(',')[1]);
                return _openWeatherMapApiService.GetForecastByCoordinate(lat, lon);
            });
        }
    }
}
