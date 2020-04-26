using AppWeather.Api.Framework.Controllers;
using AppWeather.Api.Messaging.Model.Weather;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AppWeather.Api.Controllers
{
    /// <summary>
    ///     Weather forecast api
    /// </summary>
    public class WeatherController : BaseApiController
    {
        /// <summary>
        ///     Places Controller Ctor.
        /// </summary>
        /// <param name="bus">Message buss</param>
        public WeatherController(IMediator bus)
            : base(bus)
        {
        }

        /// <summary>
        ///  Get the weather forecast by the given parameter, with the precedence in the following order: cityName, zipCode, geoCode
        /// </summary>
        /// <param name="cityName">The city name</param>
        /// <param name="zipCode">The city zip code</param>
        /// <param name="geoCode">The geo code e.g. latitude,longitude</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Forecast([FromQuery]string cityName, [FromQuery]string zipCode, [FromQuery]string geoCode)
        {

            if (Request.Query.ContainsKey("cityName"))
            {
                return ToResult(await Bus.Send(new GetForecastByCityNameRequest { CityName = cityName, UserId = UserId }));

            }
            else if (Request.Query.ContainsKey("zipCode"))
            {
                return ToResult(await Bus.Send(new GetForecastByZipCodeRequest { ZipCode = zipCode, UserId = UserId }));
            }
            else if (Request.Query.ContainsKey("geoCode"))
            {
                return ToResult(await Bus.Send(new GetForecastByCoordinateRequest { Coordinate = geoCode, UserId = UserId }));
            }
            else
            {
                return NotFound(new { error = "There is no implemented API for the given request parameters!" });
            }
        }
    }
}