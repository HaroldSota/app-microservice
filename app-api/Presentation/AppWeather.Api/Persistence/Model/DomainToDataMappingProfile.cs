using AppWeather.Api.Domain.UserSearchModel;
using AutoMapper;

namespace AppWeather.Api.Persistence.Model
{
    public class DomainToDataMappingProfile : Profile
    {
        public DomainToDataMappingProfile()
        {
            CreateMap<UserSearch, UserSearchData>();
        }
    }
}
