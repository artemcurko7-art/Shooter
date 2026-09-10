using System;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace Game.Scripts.PoolMono
{
    public abstract class PoolMono<T> where T : MonoBehaviour
    {
        //private T[] _prefabs;
        private readonly ObjectPool<T> _pool;
        private readonly DiContainer _container;
        private T _prefab;
        private int _value;

        public PoolMono(DiContainer container)
        {
            _container = container;

            _pool = Create();
        }
        
        public void SetPrefabs(T prefab)//T[] prefabs)
        {
            //_prefabs = prefabs ?? throw new ArgumentNullException(nameof(prefabs));
            _prefab = prefab;
        }

        public T Get() =>
            _pool.Get();

        protected virtual void ActionOnGet(T prefab) =>
            prefab.gameObject.SetActive(true);

        protected virtual void ActionOnRelease(T prefab) =>
            prefab.gameObject.SetActive(false);

        protected virtual void OnRelease(T prefab) =>
            _pool.Release(prefab);

        protected virtual T GetRandomPrefab()
        {
            return _prefab;
            //return _prefabs[Random.Range(0, _prefabs.Length)];
        }

        protected virtual ObjectPool<T> Create()
        {
            return new ObjectPool<T>(
                createFunc: () =>
                    _container.InstantiatePrefabForComponent<T>(GetRandomPrefab(), Vector3.zero, Quaternion.identity, null),
                actionOnGet: (prefab) => ActionOnGet(prefab),
                actionOnRelease: (prefab) => ActionOnRelease(prefab));
        }
    }
}