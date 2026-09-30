using System;
using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.Equipment.CharacterContext.Data;
using Game.Scripts.Equipment.CharacterContext.Type;
using Game.Scripts.Equipment.EquipmentContext.Data;
using Game.Scripts.Equipment.EquipmentContext.Type;
using Game.Scripts.Factory;
using UnityEngine;

namespace Game.Scripts.Service.Equipment.CharacterContext
{
    public class CharacterSlotRewardService : SlotRewardService<CharacterData, CharacterSlotFactory>
    {
        public CharacterSlotRewardService(
            RarityEquipmentData rarityData,
            CharacterData data,
            CharacterSlotFactory factory,
            Transform container)
            : base(rarityData, data, factory, container) { }

        public event Action<CharacterSlot> Added;

        public override void Execute()
        {
            //RarityEquipmentType rarityEquipmentType = WeightedRandomSampling.GetRandomWeighted<RarityEquipmentType>();
            
            var view = Factory.Create(RarityData.Configs[RarityEquipmentType.Mythical], Data.Characters[CharacterType.AttackAircraft], Container);
            var view2 = Factory.Create(RarityData.Configs[RarityEquipmentType.Mythical], Data.Characters[CharacterType.Physician], Container);
            Added?.Invoke(view);
            Added?.Invoke(view2);
        }
    }
}