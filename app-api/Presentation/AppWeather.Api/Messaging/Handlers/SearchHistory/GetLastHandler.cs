using AppWeather.Api.Messaging.Model;
using AppWeather.Api.Messaging.Model.SearchHistory;
using AppWeather.Api.Persistence;
using AppWeather.Api.Persistence.Model;
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
    public sealed class GetLastHandler : BaseHandler, IRequestHandler<GetLastRequest, QueryResponse<GetLastResponse[]>>
    {
        private readonly IDbContext _dbContext;
        private List<string> Errors { get; set; } = new List<string>();

        public GetLastHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<QueryResponse<GetLastResponse[]>> Handle(GetLastRequest query, CancellationToken cancellationToken)
        {
            try
            {
                if (!Validate(query))
                {
                    return new QueryResponse<GetLastResponse[]>(MessageType.Validation, new QueryResponseError("Validate", Errors.First()));
                }

                return new QueryResponse<GetLastResponse[]>(await _dbContext.Set<UserSearchData>()
                        .Where(item => item.UserId.Equals(query.UserId))
                        .OrderByDescending(item => item.SearchTime)
                        .Take(query.Count)
                        .Select(item => new GetLastResponse()
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
                return new QueryResponse<GetLastResponse[]>(MessageType.Error, ex);
            }
        }

        private bool Validate(GetLastRequest query)
        {
            if (string.IsNullOrEmpty(query.UserId))
                Errors.Add("User Id can not be empty");
            if (query.Count <= 0)
                Errors.Add("The number of returned item can not be less than 1");
            return Errors.Count == 0;
        }
    }
}