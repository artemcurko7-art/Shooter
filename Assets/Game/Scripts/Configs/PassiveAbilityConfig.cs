using Game.Scripts.PassiveAbility.Type;
using UnityEngine;

namespace Game.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Source/Config/PassiveAbility", fileName = "PassiveAbility", order = 9)]
    public class PassiveAbilityConfig : ScriptableObject
    {
        [field: SerializeField] public PassiveAbilityType Type { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public float Value { get; private set; }
        [field: SerializeField] public float Cooldown { get; private set; }
    }
}