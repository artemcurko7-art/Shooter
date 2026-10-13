using System.Collections.Generic;
using Game.Scripts.MV.StatContext;
using Game.Scripts.MV.StatContext.Type;

namespace Game.Scripts.Equipment.CharacterContext
{
    public interface ICharacterStatResolver
    {
        public IReadOnlyDictionary<Character, IReadOnlyDictionary<StatType, Stat>> Characters { get; }
    }
}