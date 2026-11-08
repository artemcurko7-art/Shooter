using System;
using System.Collections.Generic;
using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.PassiveAbilityContext.Data;

namespace Game.Scripts.PassiveAbilityContext.Passive
{
    public interface IPassiveAbility
    {
        public event Action<ICharacter, PassiveAbilityData> Applied;
        
        public void Apply(ICharacter character, PassiveAbilityData passiveAbilityData);
    }
}