using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Scripts.GameWorldContext;
using Game.Scripts.PlayerContext;
using Game.Scripts.PoolMono.Repository.Unit;
using Game.Scripts.Service.Subscriber;
using Game.Scripts.UnitContext;
using Game.Scripts.Wave;

namespace Game.Scripts.Service.UnitContext
{
    public class UnitService : IUnitService, ISubscriber
    {
        private readonly WaveEntry[] _waveEntries;
        private readonly IUnitPoolRepository _poolRepository;
        private readonly ITransformable _transformable;
        private CancellationTokenSource _cancellationTokenSource;

        public event Action<Unit> Added;
        
        public UnitService(GameWorld gameWorld, IUnitPoolRepository poolRepository, ITransformable transformable)
        {
            _waveEntries = gameWorld.Config.Waves;
            _poolRepository = poolRepository;
            _transformable = transformable;

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
        
        private async UniTaskVoid Spawn(CancellationToken token)
        {
            foreach (var waveEntry in _waveEntries)
            {
                if (_cancellationTokenSource.IsCancellationRequested == false)
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(waveEntry.Cooldown), cancellationToken: token);

                    foreach (var wave in waveEntry.Waves)
                    {
                        for (int i = 0; i < wave.MaxCount; i++)
                        {
                            await UniTask.Delay(TimeSpan.FromSeconds(wave.Cooldown), cancellationToken: token);
                            
                            var pools = _poolRepository.Pools[wave.Type];
                            var pool = pools[UserUtils.NumberGeneration.GetIntegerRandom(0, pools.Count - 1)];
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