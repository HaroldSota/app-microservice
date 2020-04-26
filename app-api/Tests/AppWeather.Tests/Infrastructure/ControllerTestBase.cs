using AppWeather.Tests.Infrastructure;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Xunit;

namespace AppWeather.IntegrationTest.Infrastructure
{
    public abstract class ControllerTestBase : IClassFixture<TestServerFixture>
    {
        protected readonly TestServerFixture Fixture;
        private string _controllerPath;

        public ControllerTestBase(TestServerFixture fixture, string controllerPath)
        {
            Fixture = fixture;
            _controllerPath = controllerPath;
        }

        public string GetQueryString(object obj)
        {
            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null)?.ToString());

            return string.Join("&", properties.ToArray());
        }

        protected async Task<HttpResponseMessage> GetAsync(string action)
        {
            return await Fixture.Client.GetAsync($"{_controllerPath}{action}");
        }

        protected async Task<HttpResponseMessage> GetAsync(string action, object obj)
        {
            return await Fixture.Client.GetAsync($"{_controllerPath}{action}?{GetQueryString(obj)}");
        }
    }
}