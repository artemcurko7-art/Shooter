using Game.Scripts.Equipment;
using Game.Scripts.Equipment.Type;
using UnityEngine;

namespace Game.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Source/Config/Equipment", fileName = "Equipment", order = 5)]
    public class EquipmentConfig : ScriptableObject
    {
        [field: SerializeField] public EquipmentType Type { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}