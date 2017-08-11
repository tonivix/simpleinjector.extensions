using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SimpleInjector;

namespace RTI.SimpleInjector.Extensions
{
    public static class Extensions
    {
        #region Private Methods

        private static IEnumerable<Type> FilterTypesByNamespace(IEnumerable<Type> types, string[] namespaceFilter)
        {
            return types.Where(t => namespaceFilter.Any(n => t.Namespace == n));
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Registers Services to Implementations using reflection based on the attribute 'DependencyInjection'
        /// </summary>
        /// <param name="container">The SimpleInjector Container</param>
        /// <param name="assembly">The assembly where the implementations are defined</param>
        /// <param name="lifestyle">SimpleInjector LifeStyle</param>
        /// <param name="namespaceFilter">Optionally filter types by one or more namespaces</param>
        public static void RegisterByAttribute(this Container container, Assembly assembly, Lifestyle lifestyle, params string[] namespaceFilter)
        {
            var types = assembly.GetTypes().Where(type => type.GetCustomAttributes(typeof(DependencyInjectionAttribute), true).Length > 0);

            if (namespaceFilter.Length > 0)
                types = FilterTypesByNamespace(types, namespaceFilter);

            foreach (var implementationType in types)
            {
                var attrib = implementationType.GetCustomAttributes<DependencyInjectionAttribute>().First();
                container.Register(attrib.InterfaceType, implementationType, lifestyle);
            }
        }

        /// <summary>
        ///     Registers Services to Implementations using reflection based on 'Convention over Configuration'
        ///     (the naming convention is: '[Classname] : I[Classname]'. Ex: 'class UserService : IUserService')
        /// </summary>
        /// <param name="container">The SimpleInjector Container</param>
        /// <param name="assembly">The assembly where the implementations are defined</param>
        /// <param name="lifestyle">SimpleInjector LifeStyle</param>
        /// <param name="namespaceFilter">Optionally filter types by one or more namespaces</param>
        public static void RegisterByConvention(this Container container, Assembly assembly, Lifestyle lifestyle, params string[] namespaceFilter)
        {
            var types = assembly.GetTypes().Where(type => type.GetInterface($"I{type.Name}") != null);

            if (namespaceFilter.Length > 0)
                types = FilterTypesByNamespace(types, namespaceFilter);

            foreach (var implementationType in types)
                container.Register(implementationType.GetInterface($"I{implementationType.Name}"), implementationType, lifestyle);
        }

        #endregion
    }
}