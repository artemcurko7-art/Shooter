using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Scripts.Factory;
using Game.Scripts.GameWorldContext;
using Game.Scripts.PhysicalBody.UnitContext;
using Game.Scripts.PhysicalBody.UnitContext.Attacker;
using Game.Scripts.PhysicalBody.UnitContext.Data;
using Game.Scripts.PhysicalBody.UnitContext.Type;
using Game.Scripts.PlayerContext;
using Game.Scripts.PoolMono;
using Game.Scripts.Service.Subscriber;
using Game.Scripts.Wave;
using UnityEngine;

namespace Game.Scripts.Service.UnitContext
{
    public class UnitService : IUnitService, ISubscriber
    {
        private const int Milliseconds = 1000;
        
        private readonly WaveEntry[] _waveEntries;
        private readonly UnitPool[] _pools;
        private readonly UnitFactory _factory;
        private readonly IUnitAttacker[] _attackers;
        private readonly ITransformable _transformable;
        private readonly List<Unit> _units = new();
        private readonly Transform[] _transforms;
        private CancellationTokenSource _cancellationTokenSource;

        public event Action<Unit> Added;
        
        public UnitService(
            GameWorld gameWorld,
            UnitData data,
            UnitPool[] pools,
            UnitFactory factory,
            IUnitAttacker[] attackers,
            ITransformable transformable,
            Transform[] transforms)
        {
            _waveEntries = gameWorld.Config.Waves;
            _pools = pools;
            _factory = factory;
            _attackers = attackers;
            _transformable = transformable;
            _transforms = transforms;

            foreach (var pool in pools)
                foreach (var config in data.Units[pool.Type])
                    pool.SetPrefab(config.Unit);

            foreach (var waveEntry in _waveEntries)
                foreach (var wave in waveEntry.Waves)
                    MaxCount += wave.MaxCount;
        }

        public int MaxCount { get; }
        
        public void Subscribe()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            Spawn(_cancellationTokenSource.Token).Forget();
        }

        public void Unsubscribe()
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
            foreach (var waveEntry in _waveEntries)
            {
                if (_cancellationTokenSource.IsCancellationRequested == false)
                {
                    await UniTask.Delay((int)waveEntry.Cooldown * Milliseconds, cancellationToken: token);

                    foreach (var wave in waveEntry.Waves)
                    {
                        for (int i = 0; i < wave.MaxCount; i++)
                        {
                            await UniTask.Delay((int)wave.Cooldown * Milliseconds, cancellationToken: token);

                            foreach (var pool in _pools)
                                if (wave.Type == pool.Type)
                                    Added?.Invoke(pool.Get());
                            
                            await UniTask.Yield();
                        }
                    }
                    
                    await UniTask.Yield();
                }
            }
        }
    }
}