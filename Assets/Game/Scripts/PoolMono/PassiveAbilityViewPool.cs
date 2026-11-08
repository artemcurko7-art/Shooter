using Game.Scripts.Factory;
using Game.Scripts.MV.StatContext.Modifer;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.PassiveAbilityContext;
using Game.Scripts.PassiveAbilityContext.Data;
using Game.Scripts.PassiveAbilityContext.Type;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace Game.Scripts.PoolMono
{
    public class PassiveAbilityViewPool : PoolMono<PassiveAbilityView>
    {
        private readonly IPassiveAbilityViewData _data;
        private readonly PassiveAbilityViewFactory _factory;
        private readonly PassiveAbilityData _abilityData;
        private readonly IStatModifer _statModifer;
        private Transform _container;
        
        public PassiveAbilityViewPool(
            IPassiveAbilityViewData data,
            PassiveAbilityViewFactory factory,
            PassiveAbilityData abilityData,
            IStatModifer statModifer,
            Transform transform,
            DiContainer container)
            : base(container)
        {
            _data = data;
            _factory = factory;
            _abilityData = abilityData;
            _statModifer = statModifer;
            _container = transform;
        }
        
        protected override void ActionOnGet(PassiveAbilityView view)
        {
            base.ActionOnGet(view);
            var config = _data.Views[_abilityData.StatType];
            view.Initialize(_container.position + config.Text.Offset);
            //view.Initialize(config.Text.Color, _abilityData.Value, config.Text.JumpHeight, config.Duration);
            
            if (_abilityData.Type == PassiveAbilityType.RegenerationHealth)
                view.Initialize(config.Text.Color, _statModifer.Stats[_abilityData.StatType], config.Text.JumpHeight, config.Duration);
            else
                view.Initialize(config.Text.Color, _abilityData.Value, config.Text.JumpHeight, config.Duration);
            
            view.Released += OnRelease;
        }

        public void SetTransform(Transform container)
        {
            _container = container;
        }

        protected override void ActionOnRelease(PassiveAbilityView view)
        {
            base.ActionOnRelease(view);
            view.ResetSettings();
        }

        protected override void OnRelease(PassiveAbilityView view)
        {
            base.OnRelease(view);
            view.Released -= OnRelease;
        }

        protected override ObjectPool<PassiveAbilityView> Create()
        {
            return new ObjectPool<PassiveAbilityView>(
                createFunc: () => 
                    _factory.Create(_abilityData, _container),
                actionOnGet: (prefab) => ActionOnGet(prefab),
                actionOnRelease: (prefab) => ActionOnRelease(prefab));
        }
    }
}