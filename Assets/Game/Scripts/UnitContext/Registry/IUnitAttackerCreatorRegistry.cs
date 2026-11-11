using System.Collections.Generic;
using Game.Scripts.UnitContext.Type;

namespace Game.Scripts.UnitContext.Registry
{
    public interface IUnitAttackerCreatorRegistry
    {
        IReadOnlyDictionary<UnitAttackerType, System.Type> Types { get; }
    }
}