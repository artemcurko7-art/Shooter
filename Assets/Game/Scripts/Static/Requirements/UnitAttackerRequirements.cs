using System.Collections.Generic;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.UnitContext.Type;

namespace Game.Scripts.Static.Requirements
{
    public static class UnitAttackerRequirements
    {
        public static readonly IReadOnlyDictionary<UnitAttackerType, StatType[]> Requirements = 
            new Dictionary<UnitAttackerType, StatType[]>
            {
                { UnitAttackerType.Melee, new[] { StatType.Damage, StatType.Distance, StatType.Radius } },
                { UnitAttackerType.Thrower, new[] { StatType.Damage, StatType.Speed, StatType.Radius } },
                { UnitAttackerType.AreaDamage, new[] { StatType.Damage, StatType.Radius } },
                { UnitAttackerType.AreaDamageOverTime, new[] { StatType.Damage, StatType.Radius } },
            };
    }
}