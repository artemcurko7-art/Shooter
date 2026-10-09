using Game.Scripts.Equipment.CharacterContext.Type;
using UnityEngine;

namespace Game.Scripts.Equipment.CharacterContext
{
    public interface IAttacker
    {
        public CharacterType CharacterType { get; }
        public Transform Transform { get; }
    }
}