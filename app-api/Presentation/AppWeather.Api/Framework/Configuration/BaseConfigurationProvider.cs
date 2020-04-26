using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace AppWeather.Api.Framework.Configuration
{
    /// <summary>
    ///     BaseConfigurationProvider
    /// </summary>
    public abstract class BaseConfigurationProvider
    {
        /// <summary>
        ///     Represents a set of key/value application configuration properties.
        /// </summary>
        protected readonly IConfiguration Configuration;
        private readonly string _prefix;

        /// <summary>
        ///     BaseConfigurationProvider ctor.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="prefix"></param>
        public BaseConfigurationProvider(IConfiguration configuration, string prefix)
        {
            if (!string.IsNullOrEmpty(prefix))
                _prefix = $"{prefix}{(prefix.EndsWith(':') ? string.Empty : ":")}";
            Configuration = configuration;
        }

        /// <summary>
        ///  Get configuration by selector
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selector"></param>
        /// <returns></returns>
        protected T GetConfiguration<T>(string selector)
        {
            return Configuration.GetValue<T>(GetQualifiedSelector(selector));
        }

        /// <summary>
        ///     Get configuration by selector with default value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selector"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        protected T GetConfiguration<T>(string selector, T defaultValue)
        {
            return Configuration.GetValue<T>(GetQualifiedSelector(selector), defaultValue);
        }

        /// <summary>
        ///     Get section by selector
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        protected IConfigurationSection GetSection(string selector)
        {
            return Configuration.GetSection(GetQualifiedSelector(selector));
        }

        /// <summary>
        ///      Get section  of array type by selector
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selector"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        protected T[] GetArrayConfiguration<T>(string selector, Func<IConfiguration, string, T> func)
        {
            var result = new List<T>();
            var resources = GetSection(selector).GetChildren();

            foreach (var resource in resources)
                result.Add(func(resource, string.Empty));

            return result.ToArray();

        }

        /// <summary>
        ///  Create selector from path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        protected virtual string GetQualifiedSelector(string path) => $"{_prefix}{path}";
    }
}
