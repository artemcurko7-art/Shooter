using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Scripts.Factory;
using Game.Scripts.PhysicalBody.UnitContext;
using Game.Scripts.PhysicalBody.UnitContext.Attacker;
using Game.Scripts.PhysicalBody.UnitContext.Data;
using Game.Scripts.PhysicalBody.UnitContext.Type;
using Game.Scripts.PlayerContext;
using Game.Scripts.PoolMono;
using UnityEngine;

namespace Game.Scripts.Service.PhysicalBody
{
    public class UnitService : PhysicalBodyService<Unit>
    {
        private readonly UnitType _type;
        private readonly UnitPool[] _pools;
        private readonly UnitFactory _factory;
        private readonly IUnitAttacker[] _attackers;
        private readonly ITransformable _transformable;
        private readonly List<Unit> _units = new();
        private readonly Transform[] _transforms;
        private CancellationTokenSource _cancellationTokenSource;
        private int _amount;

        public UnitService(UnitData data, UnitPool[] pools, UnitFactory factory, IUnitAttacker[] attackers, ITransformable transformable, Transform[] transforms, float delay) : base(delay)
        {
            _pools = pools;
            _factory = factory;
            _attackers = attackers;
            _transformable = transformable;
            _transforms = transforms;

            foreach (var pool in pools)
                foreach (var config in data.Units[pool.Type])
                    pool.SetPrefab(config.Unit);
        }
        
        public override void Subscribe()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            Spawn(_cancellationTokenSource.Token).Forget();
        }

        public override void Unsubscribe()
        {
            _cancellationTokenSource.Cancel();
        }

        public void OnClick(UnitType type)
        {
            foreach (var pool in _pools)
                if (pool.Type == type)
                    pool.Get();
        }
        
        private async UniTaskVoid Spawn(CancellationToken token)
        {
            while (_cancellationTokenSource.IsCancellationRequested == false && _amount < 1) // test убрать amount
            {
                await UniTask.Delay((int)Delay * 1000, cancellationToken: token);
                
                //_pool.Get();
                
                _amount++;

                await UniTask.Yield();
            }
        }
    }
}