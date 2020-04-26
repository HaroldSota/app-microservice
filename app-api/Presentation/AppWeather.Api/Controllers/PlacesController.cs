using AppWeather.Api.Framework.Controllers;
using AppWeather.Api.Messaging.Model.Places;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AppWeather.Api.Controllers
{
    /// <summary>
    ///     Routs the request to 'Google Place Autocomplete' 
    /// </summary>
    public class PlacesController : BaseApiController
    {
        /// <summary>
        ///     Places Controller Ctor.
        /// </summary>
        /// <param name="bus">Message buss</param>
        public PlacesController(IMediator bus)
            : base(bus)
        {
        }

        /// <summary>
        ///    Get Suggestion  
        /// </summary>
        /// <param name="cityName">Provide the search text</param>
        /// <returns>Return a list of suggestions</returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]string cityName)
        {
            return ToResult(await Bus.Send(new GetSuggestionRequest { CityName = cityName }));
        }
    }
}
