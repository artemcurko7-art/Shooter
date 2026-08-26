using Game.Scripts.Equipment.Type;
using Game.Scripts.WeaponContext.Type;
using NaughtyAttributes;
using UnityEngine;

namespace Game.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Source/Config/Equipment", fileName = "Equipment", order = 5)]
    public class EquipmentConfig : ScriptableObject
    {
        [field: SerializeField] public EquipmentType Type { get; private set; }
        [field: ShowIf("Type", EquipmentType.Weapon)]
        [field: SerializeField] public WeaponType WeaponType { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        
    }
}