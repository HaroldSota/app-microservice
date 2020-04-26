using AppWeather.Api.Framework;
using AppWeather.Api.Domain.UserSearchModel;
using AppWeather.IntegrationTest.Infrastructure;
using AppWeather.Api.Persistence.Model;
using AppWeather.Tests.Infrastructure;
using FluentAssertions;
using System;
using System.Linq;
using AppWeather.Api.Persistence.Model.UserSearch;
using Xunit;

namespace AppWeather.Tests.Persistence
{
    [CollectionDefinition("UserSearchRepositoryTests", DisableParallelization = true)]
    public class UserSearchRepositoryTests : ControllerTestBase
    {

        public UserSearchRepositoryTests()
            : base(new TestServerFixture(), string.Empty)
        {
            Seed();
        }


        [Fact]
        public void Test_Add_New_Item_In_Repository()
        {
            //Act
            IUserSearchRepository userRepository = EngineContext.Current.Resolve<IUserSearchRepository>();
            var user = new UserSearch(Guid.NewGuid().ToString(), "Leipzig", 13.3f, 69, DateTime.Now);
            userRepository.Add(user);


            //Assert
            var dbUser = GetLastSearch();

            dbUser.Should().NotBeNull();
            dbUser.UserId.Should().Be(user.UserId);
            dbUser.CityName.Should().Be(user.CityName);
            dbUser.Temperature.Should().Be(user.Temperature);
            dbUser.Humidity.Should().Be(user.Humidity);
            dbUser.SearchTime.Should().Be(user.SearchTime);
        }



        private UserSearchData GetLastSearch() =>
            TestDbContext.DbContext.Set<UserSearchData>().OrderByDescending(item => item.SearchTime).FirstOrDefault();
        private void Seed()
        {
            var set = TestDbContext.DbContext.Set<UserSearchData>();

            //parallel testing

            set.RemoveRange(set.ToList());

            set.AddRange(new[] {
                    new UserSearchData() { Id = Guid.NewGuid(), UserId = "TestUserId", CityName = "Berlin", Temperature = 13, Humidity = 20, SearchTime = DateTime.Now.AddSeconds(-1) },
                    new UserSearchData() { Id = Guid.NewGuid(), UserId = "TestUserId", CityName = "Hamburg", Temperature = 15, Humidity = 65, SearchTime = DateTime.Now.AddSeconds(-2) },
                    new UserSearchData() { Id = Guid.NewGuid(), UserId = "TestUserId", CityName = "München", Temperature = 21, Humidity = 71, SearchTime = DateTime.Now.AddSeconds(-3) },
                    new UserSearchData() { Id = Guid.NewGuid(), UserId = "TestUserId", CityName = "Köln", Temperature = 7, Humidity = 33, SearchTime = DateTime.Now.AddSeconds(-4) },
                    new UserSearchData() { Id = Guid.NewGuid(), UserId = "TestUserId", CityName = "Stuttgart", Temperature = -9, Humidity = 22, SearchTime = DateTime.Now.AddSeconds(-5) },
                    new UserSearchData() { Id = Guid.NewGuid(), UserId = "TestUserId", CityName = "Düsseldorf", Temperature = 8, Humidity = 85, SearchTime = DateTime.Now.AddSeconds(-6) },
            });

            TestDbContext.DbContext.SaveChanges();
        }
    }
}
