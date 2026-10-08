using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Extensions;
using Game.Scripts.MV.StatContext.Type;

namespace Game.Scripts.MV.StatContext.Repository
{
    public class StatTypeRegistry : IStatTypeRegistry
    {
        public StatTypeRegistry()
        {
            Types = ReflectionExtensions.GetImplementations<Stat>()
                .Select(type => 
                {
                    var dummyInstance = (Stat)Activator.CreateInstance(type, 0, false);
                    return new { ClassType = type, StatType = dummyInstance.Type };
                })
                .ToDictionary(types => types.StatType, types => types.ClassType);
        }

        public IReadOnlyDictionary<StatType, System.Type> Types { get; }
    }
}