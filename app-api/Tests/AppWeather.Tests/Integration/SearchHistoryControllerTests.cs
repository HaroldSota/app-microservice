using AppWeather.Api.Messaging.Model;
using AppWeather.Api.Messaging.Model.SearchHistory;
using AppWeather.IntegrationTest.Infrastructure;
using AppWeather.Api.Persistence.Model;
using AppWeather.Tests.Infrastructure;
using FluentAssertions;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;
namespace AppWeather.Tests.Integration
{
    [CollectionDefinition("SearchHistoryControllerTests", DisableParallelization = true)]
    public class SearchHistoryControllerTests : ControllerTestBase
    {

        public SearchHistoryControllerTests()
            : base(new TestServerFixture(), "api/searchhistory/")
        {
            Seed();
        }


        [Fact]
        public async Task Get_Last_Two()
        {

            //Act
            var response = await GetAsync("getlast?count=2");

            //Assert
            var result = response.GetObject<GetLastResponse[]>();
            response.StatusCode.Should().Be(HttpStatusCode.OK);


            result.Should().NotBeNull();
            result.Length.Should().Be(2);
            result.First().CityName.Should().Be("Berlin");
            result.Last().CityName.Should().Be("Hamburg");
        }

        [Fact]
        public async Task Get_Last_Negative_Count_0()
        {

            //Act
            var response = await GetAsync("getlast?count=0");

            //Assert
            var result = response.GetObject<QueryResponseError>();

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            result.Should().NotBeNull();
            result.Message.Should().Be("The number of returned item can not be less than 1");
        }

        [Fact]
        public async Task Get_All()
        {

            //Act
            var response = await GetAsync("getall");

            //Assert
            var result = response.GetObject<GetAllResponse[]>();
            response.StatusCode.Should().Be(HttpStatusCode.OK);


            result.Should().NotBeNull();
            result.Length.Should().Be(6);
            result.First().CityName.Should().Be("Berlin");
            result.Last().CityName.Should().Be("Düsseldorf");
        }

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