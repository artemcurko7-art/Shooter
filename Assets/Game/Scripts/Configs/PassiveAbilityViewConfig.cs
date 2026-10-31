using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.PassiveAbilityContext.Context;
using UnityEngine;

namespace Game.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Source/Config/PassiveAbilityView", fileName = "PassiveAbilityView", order = 9)]
    public class PassiveAbilityViewConfig : ScriptableObject
    {
        [field: SerializeField] public StatType StatType { get; private set; }
        [field: SerializeField] public EffectPassiveAbilityViewContext SelectedEffect { get; private set; }
        [field: SerializeField] public EffectPassiveAbilityViewContext UppedEffect { get; private set; }
        [field: SerializeField] public TextPassiveAbilityViewContext Text { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
    }
}