using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.VFX.Type;

namespace Game.Scripts.VFX.Data
{
    public interface IEffectData
    {
        public IReadOnlyDictionary<EffectType, EffectConfig> Effects { get; }
    }
}