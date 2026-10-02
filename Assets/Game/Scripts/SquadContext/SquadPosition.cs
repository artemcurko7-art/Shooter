using Game.Scripts.SquadContext.Type;
using UnityEngine;

namespace Game.Scripts.SquadContext
{
    public class SquadPosition : MonoBehaviour
    {
        [field: SerializeField] public SquadNumberType Type { get; private set; }
    }
}