using Game.Scripts.Common.Interfaces;
using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.PassiveAbilityContext.Data;
using Game.Scripts.PoolMono.Repository.Effect;
using Game.Scripts.PoolMono.Repository.PassiveAbilityView;
using Game.Scripts.Service.PassiveAbilityContext;
using Game.Scripts.Service.Subscriber;
using Game.Scripts.VFX;
using UnityEngine;

namespace Game.Scripts.PassiveAbilityContext.Applied
{
    public class EffectsApplied : AppliedObserver
    {
        private readonly IPassiveAbilityViewData _passiveAbilityViewData;
        private readonly IEffectPoolRepository _repository;
        
        public EffectsApplied(
            IPassiveAbilityViewData passiveAbilityViewData,
            IEffectPoolRepository repository,
            IPassiveAbilityService passiveAbilityService)
            : base(passiveAbilityService)
        {
            _passiveAbilityViewData = passiveAbilityViewData;
            _repository = repository;
        }

        protected override void OnApplied(IAttacker attacker, PassiveAbilityData passiveAbilityData)
        {
            var config = _passiveAbilityViewData.Views[passiveAbilityData.StatType];
            
            var selectedEffect = _repository.Pools[config.SelectedEffect.Type].Get();
            InitializeEffect(selectedEffect, attacker.Transform, config.SelectedEffect.Offset, config.SelectedEffect.Scale, config.Duration);
            
            var uppedEffect = _repository.Pools[config.UppedEffect.Type].Get();
            InitializeEffect(uppedEffect, attacker.Transform, config.UppedEffect.Offset, config.UppedEffect.Scale, config.Duration);
        }

        
        private void InitializeEffect(Effect effect, Transform container, Vector3 offset, Vector3 scale, float duration)
        {
            effect.transform.SetParent(container.parent);
            effect.Initialize(container.position + offset);
            effect.Initialize(duration);
            effect.transform.localScale = scale;
        }
    }
}