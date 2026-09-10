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
    public class UnitService : PhysicalBodyService<Unit>, IUnitService
    {
        private readonly UnitData _data;
        private readonly UnitPool _pool;
        private readonly UnitFactory _factory;
        private readonly IUnitAttacker[] _attackers;
        private readonly ITransformable _transformable;
        private readonly List<Unit> _units = new();
        private readonly Transform _transform;
        private CancellationTokenSource _cancellationTokenSource;
        private int _amount;

        public UnitService(UnitData data, UnitPool pool, UnitFactory factory, IUnitAttacker[] attackers, ITransformable transformable, Transform transform, float delay) : base(delay)
        {
            _data = data;
            _pool = pool;
            _factory = factory;
            _attackers = attackers;
            _transformable = transformable;
            _transform = transform;
        
            //pool.SetPrefabs(units);
            _pool.SetPrefabs(_data.Units[UnitType.Fighter][0].Unit);
        }

        //public Unit Unit { get; private set; }
        //public IReadOnlyList<ITransformable> Units => _pool.Units;
        public IReadOnlyList<ITransformable> Units => _units;
    
        public override void Subscribe()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            Spawn(_cancellationTokenSource.Token).Forget();
        }

        public override void Unsubscribe()
        {
            _cancellationTokenSource.Cancel();
        }
    
        private async UniTaskVoid Spawn(CancellationToken token)
        {
            while (_cancellationTokenSource.IsCancellationRequested == false && _amount < 3) // test убрать amount
            {
                await UniTask.Delay((int)Delay * 1000, cancellationToken: token);
            
                int index = UserUtils.NumberGeneration.GetRandom(0, _transform.childCount - 1);
                //var unit = _pool.Get();
                //var unit = _factory.Create(_data.Units[UnitType.Fighter][0]);
                //unit.Initialize(_transform.GetChild(index).position);
                
                //_units.Add(unit);
            
                //_amount++;

                await UniTask.Yield();
            }
        }
    }
}