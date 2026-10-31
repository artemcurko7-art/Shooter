using System.Collections.Generic;
using Game.Scripts.VFX.Type;

namespace Game.Scripts.VFX.Data
{
    public interface IEffectData
    {
        public IReadOnlyDictionary<EffectType, Effect> Effects { get; }
    }
}