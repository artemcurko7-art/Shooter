using Game.Scripts.Equipment;
using Game.Scripts.Equipment.Type;
using UnityEngine;

namespace Game.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Source/Config/RarityEquipment", fileName = "RarityEquipment", order = 6)]
    public class RarityEquipmentConfig : ScriptableObject
    {
        [field: SerializeField] public RarityEquipmentType Type { get; private set; }
        [field: SerializeField] public RarityEquipmentView View { get; private set; }
    }
}