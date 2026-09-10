using System.Collections.Generic;
using Game.Scripts.Factory;
using Game.Scripts.PlayerContext;
using Game.Scripts.PhysicalBody.UnitContext;
using Game.Scripts.PhysicalBody.UnitContext.Data;
using Game.Scripts.PhysicalBody.UnitContext.Type;
using UnityEngine.Pool;
using Zenject;

namespace Game.Scripts.PoolMono
{
    public class UnitPool : PoolMono<Unit>
    {
        private readonly UnitData _data;
        private readonly UnitFactory _factory;
        private readonly List<ITransformable> _units = new();

        public UnitPool(UnitData data, UnitFactory factory, DiContainer container) : base(container)
        {
            _data = data;
            _factory = factory;
        }
    
        public IReadOnlyList<ITransformable> Units => _units;
    
        protected override void ActionOnGet(Unit unit)
        {
            base.ActionOnGet(unit);
            unit.Disabled += OnRelease;
            _units.Add(unit);
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
            _units.Remove(unit);
        }

        protected override ObjectPool<Unit> Create()
        {
            return new ObjectPool<Unit>(
                createFunc: () => 
                    _factory.Create(_data.Units[UnitType.Fighter][0]),
                actionOnGet: (prefab) => ActionOnGet(prefab),
                actionOnRelease: (prefab) => ActionOnRelease(prefab));
        }
    }
}