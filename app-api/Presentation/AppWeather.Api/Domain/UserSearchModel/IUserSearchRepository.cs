namespace AppWeather.Api.Domain.UserSearchModel
{
    /// <summary>
    ///     IUserSearchRepository
    /// </summary>
    public interface IUserSearchRepository
    {
        /// <summary>
        ///     Inserts a new record in the UserSearch table
        /// </summary>
        /// <param name="userSearch"></param>
        void Add(UserSearch userSearch);
    }
}
