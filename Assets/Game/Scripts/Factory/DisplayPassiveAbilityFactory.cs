using Game.Scripts.Equipment.CharacterContext.Data;
using Game.Scripts.Equipment.CharacterContext.Type;
using Game.Scripts.Equipment.EquipmentContext;
using Game.Scripts.MV.StatContext.Data;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Factory
{
    public class DisplayPassiveAbilityFactory
    {
        private readonly ICharacterData _characterData;
        private readonly IStatViewData _statViewData;
        private readonly DisplayPassiveAbility _view;
        private readonly DiContainer _container;
        
        public DisplayPassiveAbilityFactory(
            ICharacterData characterData,
            IStatViewData statViewData,
            DisplayPassiveAbility view,
            DiContainer container)
        {
            _characterData = characterData;
            _statViewData = statViewData;
            _view = view;
            _container = container;
        }

        public DisplayPassiveAbility Create(CharacterType characterType, Transform container)
        {
            var view = _container.InstantiatePrefabForComponent<DisplayPassiveAbility>(_view, Vector3.zero, Quaternion.identity, container);
            var characterConfig = _characterData.Characters[characterType];
            var passiveAbilityData = characterConfig.PassiveAbilityData;

            view.Initialize(
                characterConfig.Type,
                characterConfig.Name,
                passiveAbilityData.Icon,
                passiveAbilityData.Name,
                passiveAbilityData.IconType,
                passiveAbilityData.Type,
                passiveAbilityData.TargetingType,
                passiveAbilityData.StatType,
                _statViewData.Stats[passiveAbilityData.StatType].Icon,
                passiveAbilityData.Value,
                passiveAbilityData.Cooldown,
                passiveAbilityData.Description);
            
            view.transform.localScale = Vector3.one;
            
            return view;
        }
    }
}