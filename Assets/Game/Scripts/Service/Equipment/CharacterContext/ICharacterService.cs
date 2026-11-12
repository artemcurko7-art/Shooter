using System.Collections.Generic;
using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.WeaponContext;

namespace Game.Scripts.Service.Equipment.CharacterContext
{
    public interface ICharacterService
    {
        public IReadOnlyList<Attacker> Characters { get; }
        public IReadOnlyList<WeaponView> WeaponViews { get; }
    }
}