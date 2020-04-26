using AppWeather.Api.Messaging.Model.Places;
using AppWeather.Tests.Infrastructure;
using FluentAssertions;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace AppWeather.Tests.Integration
{
    public class PlacesControllerTests : ControllerTestBase
    {
        public PlacesControllerTests()
            : base(new TestServerFixture(), "api/places/")
        {
        }

        [Fact]
        public async Task Get_Provides_City_Name()
        {
            //Act
            var response = await GetAsync("get?cityName=Berlin");

            //Assert
            var result = response.GetObject<GetSuggestionResponse>();
            response.StatusCode.Should().Be(HttpStatusCode.OK);


            result.Should().NotBeNull();
            result.SuggestionList.Count.Should().Be(1);
        }

        [Fact]
        public async Task Get_Negative_City_Name_Empty()
        {
            //Act
            var response = await GetAsync("get?cityName=");

            //Assert
            var result = response.GetObject<GetSuggestionResponse>();

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            result.Should().NotBeNull();
            result.SuggestionList.Count.Should().Be(0);
        }
    }
}