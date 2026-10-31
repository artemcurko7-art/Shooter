using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.PassiveAbilityContext;
using Game.Scripts.PassiveAbilityContext.Data;
using Game.Scripts.PoolMono.Repository.Effect;
using Game.Scripts.VFX;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Factory
{
    public class PassiveAbilityViewFactory
    {
        private readonly IPassiveAbilityViewData _data;
        private readonly PassiveAbilityView _view;
        private readonly IEffectPoolRepository _effectPoolRepository;
        private readonly DiContainer _container;
        
        public PassiveAbilityViewFactory(
            IPassiveAbilityViewData data,
            PassiveAbilityView view,
            IEffectPoolRepository effectPoolRepository,
            DiContainer container)
        {
            _data = data;
            _view = view;
            _effectPoolRepository = effectPoolRepository;
            _container = container;
        }

        public PassiveAbilityView Create(PassiveAbilityData data, Transform container)
        {
            var config = _data.Views[data.StatType];

            var view = _container.InstantiatePrefabForComponent<PassiveAbilityView>(_view, container.position, Quaternion.identity, container);
            view.Initialize(container.position + config.Text.Offset);
            view.Initialize(config.Text.Color, data.Value, config.Text.JumpHeight, config.Duration);
            
            var selectedEffect = _effectPoolRepository.Pools[config.SelectedEffect.Type].Get();
            InitializeEffect(selectedEffect, container, config.SelectedEffect.Offset, config.SelectedEffect.Scale, config.Duration);
            
            var uppedEffect = _effectPoolRepository.Pools[config.UppedEffect.Type].Get();
            InitializeEffect(uppedEffect, container, config.UppedEffect.Offset, config.UppedEffect.Scale, config.Duration);
            
            return view;
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