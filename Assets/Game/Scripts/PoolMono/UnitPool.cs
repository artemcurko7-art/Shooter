using Game.Scripts.Configs;
using Game.Scripts.Factory;
using Game.Scripts.PhysicalBody.UnitContext;
using Game.Scripts.PhysicalBody.UnitContext.Data;
using Game.Scripts.PhysicalBody.UnitContext.Type;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace Game.Scripts.PoolMono
{
    public class UnitPool : PoolMono<Unit>
    {
        private readonly UnitData _data;
        private readonly UnitFactory _factory;

        public UnitPool(UnitType type, UnitData data, UnitFactory factory, DiContainer container) : base(container)
        {
            Type = type;
            _data = data;
            _factory = factory;
        }
        
        public UnitType Type { get; }
        
        protected override void ActionOnGet(Unit unit)
        {
            base.ActionOnGet(unit);
            unit.Disabled += OnRelease;
        }

        protected override void ActionOnRelease(Unit unit)
        {
            base.ActionOnRelease(unit);
            unit.ResetSettings();
        }

        protected override void OnRelease(Unit unit)
        {
            base.OnRelease(unit);
            unit.Disabled -= OnRelease;
        }

        protected override ObjectPool<Unit> Create()
        {
            return new ObjectPool<Unit>(
                createFunc: () => 
                    _factory.Create(GetRandomConfig()),
                actionOnGet: (prefab) => ActionOnGet(prefab),
                actionOnRelease: (prefab) => ActionOnRelease(prefab));
        }

        private UnitConfig GetRandomConfig()
        {
            return _data.Units[Type][UserUtils.NumberGeneration.GetIntegerRandom(0, _data.Units[Type].Count - 1)];
        }
    }
}