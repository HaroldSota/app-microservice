using AppWeather.Api.Messaging.Model;
using AppWeather.Api.Framework.Configuration;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AppWeather.Api.Framework.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BaseApiController : ControllerBase
    {

        protected readonly IMediator Bus;
        private static string _userSearchCookieKeyName;
        private string UserSearchCookieKeyName => _userSearchCookieKeyName;

        /// <summary>
        ///     BaseApiController ctor.
        /// </summary>
        /// <param name="bus">Cmd. transportation buss</param>
        public BaseApiController(IMediator bus)
        {
            Bus = bus;

            if (string.IsNullOrEmpty(UserSearchCookieKeyName))
            {
                _userSearchCookieKeyName = EngineContext.Current.Resolve<IAppWeatherConfig>().UserSearchCookie;
            }
        }

        protected string UserId
        {
            get
            {
                if (Request.Cookies.ContainsKey(_userSearchCookieKeyName))
                    return Request.Cookies[_userSearchCookieKeyName];

                var config = EngineContext.Current.Resolve<IAppWeatherConfig>();

                if (!config.IsTesting)
                {
                    var userId = Guid.NewGuid().ToString();

                    Response.Cookies.Append(_userSearchCookieKeyName, userId);

                    return userId;
                }

                return "TestUserId";
            }

        }

        /// <summary>
        ///     Formats the response status to be sent to th client
        /// </summary>
        /// <typeparam name="TQueryResult">The result object type</typeparam>
        /// <param name="response">the wrapper of the result object</param>
        /// <returns></returns>
        protected IActionResult ToResult<TQueryResult>(QueryResponse<TQueryResult> response)
            => response.MessageType switch
            {
                MessageType.OK => Ok(response.Result),
                MessageType.NotFound => NotFound(response.Error),
                MessageType.Validation => BadRequest(response.Error),
                _ => Problem(response.Error.Message),
            };
    }
}