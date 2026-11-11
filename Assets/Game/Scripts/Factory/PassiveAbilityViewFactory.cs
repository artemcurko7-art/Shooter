using Game.Scripts.PassiveAbilityContext;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Factory
{
    public class PassiveAbilityViewFactory
    {
        private readonly PassiveAbilityView _view;
        private readonly DiContainer _container;
        
        public PassiveAbilityViewFactory(PassiveAbilityView view, DiContainer container)
        {
            _view = view;
            _container = container;
        }

        public PassiveAbilityView Create(Transform container)
        {
            var view = _container.InstantiatePrefabForComponent<PassiveAbilityView>(_view, container.position, Quaternion.identity, container.parent);
            
            return view;
        }
    }
}