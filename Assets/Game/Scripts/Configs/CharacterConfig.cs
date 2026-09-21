using Game.Scripts.CharacterContext;
using Game.Scripts.CharacterContext.Type;
using UnityEngine;

namespace Game.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Source/Config/Character", fileName = "Character", order = 1)]
    public class CharacterConfig : ScriptableObject
    {
        [field: SerializeField] public CharacterType Type { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Character Model { get; private set; }
        [field: SerializeField] public CharacterStat[] Stats { get; private set; }
    }
}