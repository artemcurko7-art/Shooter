using Game.Scripts.Configs;
using Game.Scripts.PhysicalBody.UnitContext;
using Game.Scripts.PhysicalBody.UnitContext.Data;
using Game.Scripts.PhysicalBody.UnitContext.Type;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Factory
{
    public class UnitFactory 
    {
        private readonly UnitData _data;
        private readonly DiContainer _container;
        private readonly Transform[] _transforms;
    
        public UnitFactory(UnitData data, DiContainer container, Transform[] transforms)
        {
            _data = data;
            _container = container;
            _transforms = transforms;
        }
    
        public Unit Create(UnitConfig config)
        {
            // var unit = _container.InstantiatePrefabForComponent<Unit>(config.Unit, Vector3.zero, Quaternion.identity, null);
            // unit.Initialize(new Vector3(100, 100, 100));
            //unit.transform.position = new Vector3(100, 200, 300);
            Unit unit = null;

            switch (config.Type)
            {
                case UnitType.Fighter:
                    unit = _container.InstantiatePrefabForComponent<Unit>(config.Unit, GetPosition(_transforms[0]), Quaternion.identity, null);
                    Debug.Log($"Fighter");
                    break;
                    
                case UnitType.Kamikaze:
                    unit = _container.InstantiatePrefabForComponent<Unit>(config.Unit, GetPosition(_transforms[1]), Quaternion.identity, null);
                    Debug.Log($"Kamikaze");
                    break;
                    
                case UnitType.Mage:
                    unit = _container.InstantiatePrefabForComponent<Unit>(config.Unit, GetPosition(_transforms[2]), Quaternion.identity, null);
                    Debug.Log($"Mage");
                    break;
            }
            
            unit.Initialize(config.Type, _data.UnitAttackers[config.AttackerType], config.Health, config.Damage, config.Speed, config.Distance);
            
            return unit;
        }
        
        private Vector3 GetPosition(Transform transform)
        {
            float positionX = UserUtils.NumberGeneration.GetFloatRandom(
                -transform.localScale.x, transform.localScale.x);
                        
            float positionZ = UserUtils.NumberGeneration.GetFloatRandom(
                -transform.localScale.z, transform.localScale.z);
                        
            //return new Vector3(positionX, transform.localScale.y, positionZ);

            return transform.position;
        }
    }
}
