using Game.Scripts.VFX;
using Game.Scripts.VFX.Type;
using UnityEngine;

namespace Game.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Source/Config/Effect", fileName = "Effect", order = 10)]
    public class EffectConfig : ScriptableObject
    {
        [field: SerializeField] public EffectType Type { get; private set; }
        [field: SerializeField] public Effect Effect { get; private set; }
    }
}