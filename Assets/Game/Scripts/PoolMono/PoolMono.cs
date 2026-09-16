using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace Game.Scripts.PoolMono
{
    public abstract class PoolMono<T> where T : MonoBehaviour
    {
        private readonly List<T> _prefabs = new();
        private readonly ObjectPool<T> _pool;
        private readonly DiContainer _container;
        private int _value;

        public PoolMono(DiContainer container)
        {
            _container = container;

            _pool = Create();
        }
        
        public void SetPrefab(T prefab)
        {
            _prefabs.Add(prefab ?? throw new ArgumentNullException(nameof(prefab)));
        }

        public T Get() =>
            _pool.Get();

        protected virtual void ActionOnGet(T prefab) =>
            prefab.gameObject.SetActive(true);

        protected virtual void ActionOnRelease(T prefab) =>
            prefab.gameObject.SetActive(false);

        protected virtual void OnRelease(T unit) =>
            _pool.Release(unit);

        protected virtual ObjectPool<T> Create()
        {
            return new ObjectPool<T>(
                createFunc: () =>
                    _container.InstantiatePrefabForComponent<T>(GetRandomPrefab(), Vector3.zero, Quaternion.identity, null),
                actionOnGet: (prefab) => ActionOnGet(prefab),
                actionOnRelease: (prefab) => ActionOnRelease(prefab));
        }
        
        private T GetRandomPrefab()
        {
            return _prefabs[UserUtils.NumberGeneration.GetIntegerRandom(0, _prefabs.Count - 1)];
        }
    }
}