using Game.Scripts.Equipment.CharacterContext.Handler;
using Game.Scripts.Equipment.EquipmentContext.General;
using Game.Scripts.Service.Equipment;
using Game.Scripts.Service.Equipment.EquipmentContext;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Equipment.EquipmentContext
{
    public class DisplayGeneralStat : MonoBehaviour
    {
        [SerializeField] private GeneralStat[] _stats;
        
        private GeneralStatsHandler _handler;
        
        [Inject]
        public void Construct(GeneralStatsHandler handler)
        {
            _handler = handler;
        }

        private void OnEnable()
        {
            foreach (var stat in _stats)
                 stat.SetValue(_handler.Stats[stat.Type]);
        }
    }
}