﻿using System;
using System.Collections.Generic;

namespace AppWeather.Api.Framework
{
    /// <summary>
    ///    Generic singleton dictionary
    /// </summary>
    /// <typeparam name="T">The type of object to store</typeparam>
    public static class Singleton<T>
    {
        private static T _instance;

        private static IDictionary<Type, object> AllSingletons { get; }

        static Singleton()
        {
            AllSingletons = new Dictionary<Type, object>();
        }

        public static T Instance
        {
            get => _instance;
            set
            {
                _instance = value;
                AllSingletons[typeof(T)] = value;
            }
        }
    }
}