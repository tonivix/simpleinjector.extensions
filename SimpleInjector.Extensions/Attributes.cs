using System;

namespace RTI.SimpleInjector.Extensions
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