using Game.Scripts.Equipment.EquipmentContext.Type;
using UnityEngine;

namespace Game.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Source/Config/RarityEquipment", fileName = "RarityEquipment", order = 7)]
    public class RarityConfig : ScriptableObject
    {
        [field: SerializeField] public RarityType Type { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public int MaxParameter { get; private set; }
        [field: SerializeField] public int Multiplier { get; private set; }
    }
}