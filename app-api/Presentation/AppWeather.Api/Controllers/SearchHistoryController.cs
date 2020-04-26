using AppWeather.Api.Framework.Controllers;
using AppWeather.Api.Messaging.Model.SearchHistory;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AppWeather.Api.Controllers
{
    /// <summary>
    ///     Routs the request to 'User Search' 
    /// </summary>
    public class SearchHistoryController : BaseApiController
    {
        /// <summary>
        ///     Search History Controller Ctor.
        /// </summary>
        /// <param name="bus">Message buss</param>
        public SearchHistoryController(IMediator bus)
            : base(bus)
        {
        }
        /// <summary>
        ///    Get last searches by the user
        /// </summary>
        /// <returns>Return a list of suggestions</returns>
        [HttpGet]
        public async Task<IActionResult> GetLast(int count = 5)
        {
            return ToResult(await Bus.Send(new GetLastRequest() { Count = count, UserId = UserId }));
        }

        /// <summary>
        ///    Get all searches by the user
        /// </summary>
        /// <returns>Return a list of suggestions</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return ToResult(await Bus.Send(new GetAllRequest() { UserId = UserId }));
        }
    }
}