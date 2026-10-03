using System;
using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.Equipment.CharacterContext.Data;
using Game.Scripts.Equipment.CharacterContext.Type;
using Game.Scripts.Equipment.EquipmentContext.Data;
using Game.Scripts.Equipment.EquipmentContext.Type;
using Game.Scripts.Factory;
using Game.Scripts.Service.Equipment.Reward;
using UnityEngine;

namespace Game.Scripts.Service.Equipment.CharacterContext
{
    public class CharacterSlotRewardService : SlotRewardService, ISlotRewardService<CharacterSlot>
    {
        private readonly CharacterData _data;
        private readonly CharacterSlotFactory _factory;
        
        public event Action<CharacterSlot> Rewarded;
        
        public CharacterSlotRewardService(
            RarityData rarityData,
            Transform container,
            CharacterData data,
            CharacterSlotFactory factory)
            : base(rarityData, container)
        {
            _data = data;
            _factory = factory;
        }

        public override void Execute()
        {
            //RarityEquipmentType rarityEquipmentType = WeightedRandomSampling.GetRandomWeighted<RarityEquipmentType>();
            
            var view = _factory.Create(RarityData.Configs[RarityEquipmentType.Mythical], _data.Characters[CharacterType.AttackAircraft], Container);
            var view2 = _factory.Create(RarityData.Configs[RarityEquipmentType.Mythical], _data.Characters[CharacterType.Healer], Container);
            Rewarded?.Invoke(view);
            Rewarded?.Invoke(view2);
        }
    }
}