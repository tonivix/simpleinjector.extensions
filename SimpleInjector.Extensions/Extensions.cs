using System.Linq;
using System.Reflection;
using SimpleInjector;

namespace RTI.SimpleInjector.Extensions
{
    public static class Extensions
    {
        public static void RegisterImplementationsFromAssembly(this Container container, Assembly assembly, Lifestyle lifestyle, params string[] namespaceFilter)
        {
            var types = assembly
                .GetTypes()
                .Where(type => type.GetCustomAttributes(typeof(DependencyInjectionAttribute), true).Length > 0);

            if (namespaceFilter.Length > 0)
                types = types.Where(t => namespaceFilter.Any(n => t.Namespace.StartsWith(n)));

            foreach (var implementationType in types)
            {
                var attrib = implementationType.GetCustomAttributes<DependencyInjectionAttribute>().First();
                container.Register(attrib.InterfaceType, implementationType, lifestyle);
            }
        }
    }
}
