using Game.Scripts.Configs;
using Game.Scripts.Factory;
using Game.Scripts.PhysicalBody.UnitContext;
using Game.Scripts.PhysicalBody.UnitContext.Data;
using Game.Scripts.PhysicalBody.UnitContext.Type;
using Game.Scripts.PlayerContext;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace Game.Scripts.PoolMono
{
    public class UnitPool : PoolMono<Unit>
    {
        private const float ForwardPosition = 6f;
        private const float BackPosition = 3f;
        private const float RangePosition = 5.5f;
        
        private readonly UnitData _data;
        private readonly UnitFactory _factory;
        private readonly ITransformable _transformable;

        public UnitPool(UnitType type, UnitData data, UnitFactory factory, ITransformable transformable, DiContainer container) : base(container)
        {
            Type = type;
            _data = data;
            _factory = factory;
            _transformable = transformable;
        }
        
        public UnitType Type { get; }
        
        protected override void ActionOnGet(Unit unit)
        {
            base.ActionOnGet(unit);
            unit.Initialize(GetRandomPosition());
            unit.Released += OnRelease;
        }

        protected override void ActionOnRelease(Unit unit)
        {
            base.ActionOnRelease(unit);
            unit.ResetSettings();
        }

        protected override void OnRelease(Unit unit)
        {
            base.OnRelease(unit);
            unit.Released -= OnRelease;
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

        private Vector3 GetRandomPosition()
        {
            return new Vector3(
                UserUtils.NumberGeneration.GetFloatRandom(-RangePosition, RangePosition),
                _transformable.Transform.position.y,
                UserUtils.NumberGeneration.GetFloatRandom(-BackPosition, ForwardPosition));
        }
    }
}