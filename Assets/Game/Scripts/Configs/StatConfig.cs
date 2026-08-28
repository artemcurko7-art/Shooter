using Game.Scripts.Equipment.Type;
using Game.Scripts.MV.StatContext.Data;
using UnityEngine;

namespace Game.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Source/Config/Stat", fileName = "Stat", order = 4)]
    public class StatConfig : ScriptableObject
    {
        [field: SerializeField] public EquipmentType Type { get; private set; }
        [field: SerializeField] public StatInfoData[] Stats { get; private set; }
    }
}