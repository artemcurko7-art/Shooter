using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Scripts.GameWorldContext;
using Game.Scripts.PlayerContext;
using Game.Scripts.PoolMono;
using Game.Scripts.Service.Subscriber;
using UnityEngine;

namespace Game.Scripts.Service.Map
{
    public class MapService : ISubscriber
    {
        private readonly MapPool _pool;
        private readonly ITransformable _transformable;
        private PhysicalBody.MapContext.Map _forwardMap;
        private PhysicalBody.MapContext.Map _backMap;
        private CancellationTokenSource _cancellationTokenSource;
        private Vector3 _frontCursor;
        private Vector3 _backCursor;
         
        public MapService(GameWorld gameWorld, MapPool pool, ITransformable transformable)
        {
            _pool = pool;
            _transformable = transformable;

            foreach (var map in gameWorld.Config.Maps)
                _pool.SetPrefab(map);
            
            SpawnStarted();
        }

        public void Subscribe()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            Spawn(_cancellationTokenSource.Token).Forget();
        }

        public void Unsubscribe()
        {
            _cancellationTokenSource.Cancel();
        }
        
        public void SpawnForward()
        {
            var map = _pool.Get();
            _forwardMap = map;
            
            map.Initialize(GetPosition(map.Road.transform, _frontCursor)); 
            _frontCursor += GetCalculationOnLength(map.Road.transform);
        }
        
        public void SpawnBack()
        {
            var map = _pool.Get();
            _backMap = map;
            
            map.Initialize(GetPosition(map.Road.transform, _backCursor));
            _backCursor -= GetCalculationOnLength(map.Road.transform);
        }

        private void SpawnStarted()
        {
            var map = _pool.Get();
            map.Initialize(Vector3.zero);
            _forwardMap = map;
            _backMap = map;
            
            var halfLength = map.Road.transform.localScale.z * 0.5f;
            _frontCursor = Vector3.forward * halfLength;
            _backCursor = -Vector3.forward * halfLength - GetCalculationOnLength(map.Road.transform);
        }
        
        private Vector3 GetCalculationOnLength(Transform transform)
        {
            return Vector3.forward * transform.localScale.z;
        }

        private Vector3 GetPosition(Transform transform, Vector3 cursor)
        {
            var halfLength = transform.localScale.z * 0.5f;
            var position = cursor + Vector3.forward * halfLength;
            
            return position;
        }
        
        private async UniTaskVoid Spawn(CancellationToken token)
        {
            while (_cancellationTokenSource.IsCancellationRequested == false)
            {
                await UniTask.Delay((int)0.1f * 1000, cancellationToken: token);
                
                float forwardDot = GetVectorDot(_forwardMap.Road.transform);
                float backDot = GetVectorDot(_backMap.Road.transform);

                if (forwardDot < 8f)
                    SpawnForward();
                
                if (backDot > 30f)
                    SpawnBack();
                
                await UniTask.Yield();
            }
        }

        private float GetVectorDot(Transform target)
        {
            var vectorMap = target.position - _transformable.Transform.position;
            return Vector3.Dot(_transformable.Transform.forward, vectorMap);
        }
    }
}