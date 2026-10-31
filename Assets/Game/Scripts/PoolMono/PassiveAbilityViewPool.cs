using Game.Scripts.Factory;
using Game.Scripts.PassiveAbilityContext;
using Game.Scripts.PassiveAbilityContext.Data;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace Game.Scripts.PoolMono
{
    public class PassiveAbilityViewPool : PoolMono<PassiveAbilityView>
    {
        private readonly PassiveAbilityViewFactory _factory;
        private readonly PassiveAbilityData _data;
        private readonly Transform _container;
        
        public PassiveAbilityViewPool(PassiveAbilityViewFactory factory, PassiveAbilityData data, Transform transform, DiContainer container) : base(container)
        {
            _factory = factory;
            _data = data;
            _container = transform;
        }
        
        protected override void ActionOnGet(PassiveAbilityView view)
        {
            base.ActionOnGet(view);
            view.Released += OnRelease;
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

        // protected override ObjectPool<PassiveAbilityView> Create()
        // {
        //     return new ObjectPool<PassiveAbilityView>(
        //         createFunc: () => 
        //             _factory.Create(_data, _container),
        //         actionOnGet: (prefab) => ActionOnGet(prefab),
        //         actionOnRelease: (prefab) => ActionOnRelease(prefab));
        // }
    }
}