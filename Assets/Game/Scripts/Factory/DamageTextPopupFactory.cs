using Game.Scripts.TextPopup;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Factory
{
    public class DamageTextPopupFactory
    {
        private readonly DamageTextPopup _damageTextPopup;
        private readonly DiContainer _container;
        
        public DamageTextPopupFactory(DamageTextPopup damageTextPopup, DiContainer container)
        {
            _container = container;
            _damageTextPopup = damageTextPopup;
        }

        public DamageTextPopup Create()
        {
            var value = _container.InstantiatePrefabForComponent<DamageTextPopup>(_damageTextPopup, Vector3.zero, Quaternion.identity, null);
            
            return value;
        }
    }
}