using Game.Scripts.Configs;
using Game.Scripts.Factory;
using Game.Scripts.PlayerContext;
using Game.Scripts.UnitContext;
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

        private readonly UnitConfig _config;
        private readonly UnitFactory _factory;
        private readonly ITransformable _transformable;

        public UnitPool(UnitConfig config, UnitFactory factory, ITransformable transformable, DiContainer container) : base(container)
        {
            _config = config;
            _factory = factory;
            _transformable = transformable;
            
            SetPrefab(config.Unit);
        }
        
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
                    _factory.Create(_config),
                actionOnGet: (prefab) => ActionOnGet(prefab),
                actionOnRelease: (prefab) => ActionOnRelease(prefab));
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