using AppWeather.Api.Messaging.Model;
using AppWeather.Api.Messaging.Model.SearchHistory;
using AppWeather.Api.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AppWeather.Api.Persistence.Model.UserSearch;

namespace AppWeather.Api.Messaging.Handlers.SearchHistory
{
    /// <inheritdoc cref="BaseHandler"/>
    public sealed class GetAllHandler : BaseHandler, IRequestHandler<GetAllRequest, QueryResponse<GetAllResponse[]>>
    {
        private readonly IDbContext _dbContext;
        private List<string> Errors { get; set; } = new List<string>();

        /// <summary>
        ///     GetAllHandler ctor.
        /// </summary>
        /// <param name="dbContext"></param>
        public GetAllHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<QueryResponse<GetAllResponse[]>> Handle(GetAllRequest query, CancellationToken cancellationToken)
        {
            try
            {
                if (!Validate(query))
                {
                    return new QueryResponse<GetAllResponse[]>(MessageType.Validation, new QueryResponseError("Validate", Errors.First()));
                }

                return new QueryResponse<GetAllResponse[]>(await _dbContext.Set<UserSearchData>()
                 .Where(item => item.UserId.Equals(query.UserId))
                 .OrderByDescending(item => item.SearchTime)
                 .Select(item => new GetAllResponse()
                 {
                     CityName = item.CityName,
                     Temperature = item.Temperature,
                     Humidity = item.Humidity
                 })
                 .AsNoTracking()
                 .ToArrayAsync());
            }
            catch (Exception ex)
            {
                return new QueryResponse<GetAllResponse[]>(MessageType.Error, ex);
            }
        }

        private bool Validate(GetAllRequest query)
        {
            if (string.IsNullOrEmpty(query.UserId))
                Errors.Add("User Id can not be empty");

            return Errors.Count == 0;
        }
    }
}