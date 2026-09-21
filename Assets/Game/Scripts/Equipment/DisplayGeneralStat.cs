using Game.Scripts.Equipment.General;
using Game.Scripts.Service.Equipment;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Equipment
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