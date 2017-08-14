using System.Linq;
using System.Reflection;
using RTI.SimpleInjector.Extensions.Helpers;
using SimpleInjector;

namespace RTI.SimpleInjector.Extensions
{
    public static class Extensions
    {
        /// <summary>
        ///     Registers Services to Implementations using reflection based on the attribute 'DependencyInjection'
        /// </summary>
        /// <param name="container">The SimpleInjector Container</param>
        /// <param name="assembly">The assembly where the implementations are defined</param>
        /// <param name="lifestyle">SimpleInjector LifeStyle</param>
        /// <param name="namespaceFilter">Optionally filter types by one or more namespaces</param>
        public static void RegisterByAttribute(this Container container, Assembly assembly, Lifestyle lifestyle, params string[] namespaceFilter)
        {
            var types = Reflection.GetServicesInAssemblyByAttribute<DependencyInjectionAttribute>(assembly, namespaceFilter);

            foreach (var type in types)
            {
                var attrib = type.GetCustomAttributes<DependencyInjectionAttribute>().First();
                container.Register(attrib.InterfaceType, type, lifestyle);
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
            var types = Reflection.GetTypesInAssembly(assembly,
                type => type.GetInterface($"I{type.Name}") != null, namespaceFilter);

            foreach (var type in types)
                container.Register(type.GetInterface($"I{type.Name}"), type, lifestyle);
        }
    }
}