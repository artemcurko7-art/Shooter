using Game.Scripts.VFX;
using Zenject;

namespace Game.Scripts.PoolMono
{
    public class EffectPool : PoolMono<Effect>
    {
        public EffectPool(DiContainer container) : base(container) { }
        
        protected override void ActionOnGet(Effect effect)
        {
            base.ActionOnGet(effect);
            effect.Released += OnRelease;
        }

        protected override void ActionOnRelease(Effect effect)
        {
            base.ActionOnRelease(effect);
            effect.ResetSettings();
        }

        protected override void OnRelease(Effect effect)
        {
            base.OnRelease(effect);
            effect.Released -= OnRelease;
        }
    }
}