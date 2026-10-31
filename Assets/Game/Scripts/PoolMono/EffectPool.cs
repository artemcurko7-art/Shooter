using Game.Scripts.VFX;
using Game.Scripts.VFX.Type;
using Zenject;

namespace Game.Scripts.PoolMono
{
    public class EffectPool : PoolMono<Effect>
    {
        public EffectPool(EffectType type, DiContainer container) : base(container)
        {
            Type = type;
        }
        
        public EffectType Type { get; private set; }
        
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