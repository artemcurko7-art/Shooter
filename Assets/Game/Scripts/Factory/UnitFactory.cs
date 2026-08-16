using Game.Scripts.Configs;
using Game.Scripts.PhysicalBody.UnitContext;
using Game.Scripts.PhysicalBody.UnitContext.Data;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Factory
{
    public class UnitFactory 
    {
        private readonly UnitData _data;
        private readonly DiContainer _container;
    
        public UnitFactory(UnitData data, DiContainer container)
        {
            _data = data;
            _container = container;
        }
    
        public Unit Create(UnitConfig config)
        {
            var unit = _container.InstantiatePrefabForComponent<Unit>(config.Unit, Vector3.zero, Quaternion.identity, null);
            unit.Initialize(_data.UnitAttackers[config.AttackerType], config.Health, config.Damage, config.Speed, config.Distance);
        
            return unit;
        }
    }
}
