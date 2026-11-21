using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.Equipment.EquipmentContext.Type;

namespace Game.Scripts.Common.Classes.Rarity
{
    public interface IRarityData
    {
        public IReadOnlyDictionary<RarityType, RarityConfig> Rarities { get; }
    }
}