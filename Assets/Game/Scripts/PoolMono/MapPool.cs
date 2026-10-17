using System;
using Game.Scripts.PhysicalBody.MapContext;
using UnityEngine;
using Zenject;

namespace Game.Scripts.PoolMono
{
    public class MapPool : PoolMono<Map>
    {
        private Vector3 _currentPosition;
        private int _maxSpawned;

        public MapPool(DiContainer container) : base(container) {  }
        
        // public void SetMaxSpawned(int maxSpawned)
        // {
        //     if (maxSpawned < 0)
        //         throw new IndexOutOfRangeException("maxSpawned cannot be less than 0");
        //
        //     _maxSpawned = maxSpawned;
        // }

        protected override void ActionOnGet(Map map)
        {
            base.ActionOnGet(map);
            //map.Initialize(_currentPosition);
            //_currentPosition = GetCalculationOnLength(map.transform);
            map.Released += OnRelease;
        }
        
        protected override void ActionOnRelease(Map map)
        {
            base.ActionOnRelease(map);
            map.ResetSettings();
        }
        
        protected override void OnRelease(Map map)
        {
            base.OnRelease(map);
            map.Released -= OnRelease;
        }

        // private Vector3 GetCalculationOnLength(Transform transform) =>
        //     transform.position + transform.forward * transform.localScale.z;
    }
}