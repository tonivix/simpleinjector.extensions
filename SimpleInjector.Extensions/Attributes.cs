using System;

namespace SimpleInjector.Extensions
{
    public static class Attributes
    {
        [AttributeUsage(AttributeTargets.Class)]
        public class DependencyInjectionAttribute : Attribute
        {
            public readonly Type InterfaceType;

            public DependencyInjectionAttribute(Type interfaceType)
            {
                this.InterfaceType = interfaceType;
            }
        }
    }
}