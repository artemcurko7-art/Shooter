using System.Collections.Generic;
using Game.Scripts.MV.StatContext;

namespace Game.Scripts.Equipment.CharacterContext
{
    public interface ICharacterHealthResolver
    {
        public Health Health { get; }
    }
}