using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace AppWeather.Api.Framework.Configuration
{
    public abstract class BaseConfigurationProvider
    {
        protected readonly IConfiguration _configuration;
        private readonly string _prefix;

        public BaseConfigurationProvider(IConfiguration configuration, string prefix)
        {
            if (!string.IsNullOrEmpty(prefix))
                _prefix = $"{prefix}{(prefix.EndsWith(':') ? string.Empty : ":")}";
            _configuration = configuration;
        }

        protected T GetConfiguration<T>(string selector)
        {
            return _configuration.GetValue<T>(GetQualifiedSelector(selector));
        }

        protected T GetConfiguration<T>(string selector, T defaultValue)
        {
            return _configuration.GetValue<T>(GetQualifiedSelector(selector), defaultValue);
        }

        protected IConfigurationSection GetSection(string selector)
        {
            return _configuration.GetSection(GetQualifiedSelector(selector));
        }

        protected T[] GetArrayConfiguration<T>(string selector, Func<IConfiguration, string, T> func)
        {
            var result = new List<T>();
            var resources = GetSection(selector).GetChildren();

            foreach (var resource in resources)
                result.Add(func(resource, string.Empty));

            return result.ToArray();

        }

        protected virtual string GetQualifiedSelector(string path) => $"{_prefix}{path}";
    }
}
