using Game.Scripts.Equipment.Type;
using Game.Scripts.MV.StatContext.Data;
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
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public StatInfoData[] MainStats { get; private set; }
        [field: SerializeField] public StatInfoData[] AdditionalStats { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }

        private void OnValidate()
        {
            if (MainStats == null || AdditionalStats == null)
                return;
            
            foreach (var mainStat in MainStats)
                mainStat.OnValidate();
            
            foreach (var additionalStat in AdditionalStats)
                additionalStat.OnValidate();
        }
    }
}