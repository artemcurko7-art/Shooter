using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Scripts.Extensions
{
    public static class ReflectionExtensions
    {
        public static IEnumerable<Type> GetImplementations<TInterface>()
        {
            var interfaceType = typeof(TInterface);

            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => interfaceType.IsAssignableFrom(type) 
                               && type.IsInterface == false
                               && type.IsAbstract == false);
        }
    }
}