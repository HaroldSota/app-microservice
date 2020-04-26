using AppWeather.Api.Persistence.Model.UserSearch;
using AutoMapper;

namespace AppWeather.Api.Persistence.Model
{
    /// <summary>
    ///     Provides a named configuration for maps for persistence entities.
    ///     Naming conventions become scoped per profile.
    /// </summary>
    public class DomainToDataMappingProfile : Profile
    {
        /// <summary>
        ///     DomainToDataMappingProfile ctor.
        /// </summary>
        public DomainToDataMappingProfile()
        {
            CreateMap<Domain.UserSearchModel.UserSearch, UserSearchData>();
        }
    }
}
