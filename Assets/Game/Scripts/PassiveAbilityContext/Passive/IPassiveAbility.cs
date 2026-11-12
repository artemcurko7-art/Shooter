using System;
using System.Collections.Generic;
using Game.Scripts.Common.Interfaces;
using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.PassiveAbilityContext.Data;

namespace Game.Scripts.PassiveAbilityContext.Passive
{
    public interface IPassiveAbility
    {
        public event Action<IAttacker, PassiveAbilityData> Applied;
        
        public void Apply(IAttacker attacker, PassiveAbilityData passiveAbilityData);
    }
}