using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RTI.SimpleInjector.Extensions.Helpers
{
    public static class Reflection
    {
        private static IEnumerable<Type> FilterTypesByNamespace(IEnumerable<Type> types, string[] namespaceFilter)
        {
            return types.Where(t => namespaceFilter.Any(n => t.Namespace == n));
        }

        public static IEnumerable<Type> GetTypesInAssembly(Assembly assembly, Func<Type, bool> predicate, params string[] namespaceFilter)
        {
            var types = assembly.GetTypes().Where(predicate);

            if (namespaceFilter.Length > 0)
                types = FilterTypesByNamespace(types, namespaceFilter);

            return types;
        }

        public static IEnumerable<Type> GetServicesInAssemblyByAttribute<T>(Assembly assembly, string[] namespaceFilter)
            where T : Attribute
        {
            return GetTypesInAssembly(assembly,
                type => type.GetCustomAttributes(typeof(T), true).Length > 0,
                namespaceFilter);
        }
    }
}