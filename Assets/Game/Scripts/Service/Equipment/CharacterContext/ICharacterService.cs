using System.Collections.Generic;
using Game.Scripts.WeaponContext;

namespace Game.Scripts.Service.Equipment.CharacterContext
{
    public interface ICharacterService
    {
        public IReadOnlyList<WeaponView> WeaponViews { get; }
    }
}