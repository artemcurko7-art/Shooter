using Game.Scripts.Equipment.Type;
using UnityEngine;

namespace Game.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Source/Config/RarityEquipment", fileName = "RarityEquipment", order = 6)]
    public class RarityEquipmentConfig : ScriptableObject
    {
        [field: SerializeField] public RarityEquipmentType Type { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}