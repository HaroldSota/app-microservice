using System.Net.Http;

namespace AppWeather.Tests.Infrastructure
{
    public static class HttpResponseMessageExtensions
    {
        public static T GetObject<T>(this HttpResponseMessage responseMessage)
        {
            var response = responseMessage.Content.ReadAsStringAsync().Result;

            return ClientSerializer.Deserialize<T>(response);
        }

    }
}