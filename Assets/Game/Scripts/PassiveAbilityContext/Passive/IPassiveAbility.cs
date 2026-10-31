using System;
using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.MV.StatContext.Type;

namespace Game.Scripts.PassiveAbilityContext.Passive
{
    public interface IPassiveAbility
    {
        public event Action<ICharacter> Applied;
        
        public void Apply(ICharacter character);
    }
}