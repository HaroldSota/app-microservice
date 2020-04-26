using AppWeather.Api.Domain.UserSearchModel;

namespace AppWeather.Api.Persistence.Model.UserSearch
{
    /// <inheritdoc />
    public sealed class UserSearchRepository : IUserSearchRepository
    {
        private readonly IBaseRepository<Domain.UserSearchModel.UserSearch, UserSearchData> _baseRepository;


        /// <summary>
        ///     UserSearchRepository ctor.
        /// </summary>
        /// <param name="baseRepository">The base repository containing all the CRUD actions</param>
        public UserSearchRepository(IBaseRepository<Domain.UserSearchModel.UserSearch, UserSearchData> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        /// <inheritdoc />
        public void Add(Domain.UserSearchModel.UserSearch userSearch)
        {
            _baseRepository.Add(userSearch);
        }
    }
}