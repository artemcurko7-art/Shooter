using Game.Scripts.Factory;
using Game.Scripts.TextPopup;
using UnityEngine.Pool;
using Zenject;

namespace Game.Scripts.PoolMono
{
    public class DamageTextPopupPool : PoolMono<DamageTextPopup>
    {
        private readonly DamageTextPopupFactory _factory;
        
        public DamageTextPopupPool(DamageTextPopupFactory factory, DiContainer container) : base(container)
        {
            _factory = factory;
        }
        
        protected override void ActionOnGet(DamageTextPopup text)
        {
            base.ActionOnGet(text);
            text.Released += OnRelease;
        }

        protected override void ActionOnRelease(DamageTextPopup text)
        {
            base.ActionOnRelease(text);
            text.ResetSettings();
        }

        protected override void OnRelease(DamageTextPopup text)
        {
            base.OnRelease(text);
            text.Released -= OnRelease;
        }

        protected override ObjectPool<DamageTextPopup> Create()
        {
            return new ObjectPool<DamageTextPopup>(
                createFunc: () =>
                    _factory.Create(),
                actionOnGet: (prefab) => ActionOnGet(prefab),
                actionOnRelease: (prefab) => ActionOnRelease(prefab));
        }
    }
}