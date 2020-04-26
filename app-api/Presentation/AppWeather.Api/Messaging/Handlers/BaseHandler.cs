using System.Linq;

namespace AppWeather.Api.Messaging.Handlers
{
    public abstract class BaseHandler
    {
        protected string GetCacheKey<T>(T query) where T : class
        {
            var key = typeof(T).Name;

            query.GetType().GetProperties().ToList().ForEach(prop =>
            {
                var propValue = prop.GetValue(query, null);

                if (propValue != null)
                    key = $"{key}_{propValue}";
            });

            return key;
        }
    }
}
