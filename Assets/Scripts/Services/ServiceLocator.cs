using System;
using System.Collections.Generic;

namespace JokerGames.Services
{
    public class ServiceLocator : IServiceLocator
    {
        private Dictionary<Type, object> _services = new Dictionary<Type, object>();

        
        /// <summary>
        /// Get Service from service Locator
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public T GetService<T>()
        {
            Type type = typeof(T);
            if (_services.TryGetValue(type, out var service))
            {
                return (T)service;
            }

            throw new Exception($"Service of type {type.FullName} not found.");
        }

        /// <summary>
        /// Save Service
        /// </summary>
        /// <param name="service"></param>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="Exception"></exception>
        public void RegisterService<T>(T service)
        {
            Type type = typeof(T);
            if (!_services.ContainsKey(type))
            {
                _services[type] = service;
            }
            else
            {
                throw new Exception($"Service of type {type.FullName} is already registered.");
            }
        }
    }
}
