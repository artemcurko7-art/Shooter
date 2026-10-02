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
    public class CharacterSlotRewardService : SlotRewardService<CharacterSlot, CharacterData, CharacterSlotFactory>
    {
        public CharacterSlotRewardService(
            RarityEquipmentData rarityData,
            CharacterData data,
            CharacterSlotFactory factory,
            Transform container)
            : base(rarityData, data, factory, container) { }

        public override void Execute()
        {
            //RarityEquipmentType rarityEquipmentType = WeightedRandomSampling.GetRandomWeighted<RarityEquipmentType>();
            
            var view = Factory.Create(RarityData.Configs[RarityEquipmentType.Mythical], Data.Characters[CharacterType.AttackAircraft], Container);
            OnAdded(view);
        }
    }
}