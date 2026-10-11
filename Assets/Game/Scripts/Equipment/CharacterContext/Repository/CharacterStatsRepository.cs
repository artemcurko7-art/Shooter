using System.Collections.Generic;
using Game.Scripts.Equipment.CharacterContext.Type;
using Game.Scripts.MV.StatContext.Type;

namespace Game.Scripts.MV.StatContext.Repository
{
    public class CharacterStatsRepository
    {
        private readonly Dictionary<CharacterType, IReadOnlyDictionary<StatType, Stat>> _stats = new();

        public CharacterStatsRepository()
        {

        }

        IReadOnlyDictionary<CharacterType, IReadOnlyDictionary<StatType, Stat>> Stats => _stats;
    }
}