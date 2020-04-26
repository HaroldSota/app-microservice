using AppWeather.Api.Domain.UserSearchModel;

namespace AppWeather.Api.Persistence.Model
{
    public sealed class UserSearchRepository : IUserSearchRepository
    {
        private readonly IBaseRepository<UserSearch, UserSearchData> _baseRepository;

        public UserSearchRepository(IBaseRepository<UserSearch, UserSearchData> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public void Add(UserSearch userSearch)
        {
            _baseRepository.Add(userSearch);
        }
    }
}