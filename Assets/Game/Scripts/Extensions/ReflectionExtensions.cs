using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Scripts.Extensions
{
    public static class ReflectionExtensions
    {
        public static IEnumerable<Type> GetImplementations<T>()
        {
            var interfaceType = typeof(T);

            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => interfaceType.IsAssignableFrom(type) 
                               && type.IsInterface == false
                               && type.IsAbstract == false);
        }
    }
}