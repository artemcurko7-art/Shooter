using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.Equipment.CharacterContext.Type;
using UnityEngine;

namespace Game.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Source/Config/Character", fileName = "Character", order = 2)]
    public class CharacterConfig : ScriptableObject
    {
        [field: SerializeField] public CharacterType Type { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public Character View { get; private set; }
        [field: SerializeField] public CharacterStat[] Stats { get; private set; }
    }
}