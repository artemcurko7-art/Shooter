using System;
using System.Collections;
using Game.Scripts.Damagable;
using Game.Scripts.MV.StatContext;
using UnityEngine;
using Zenject;

namespace Game.Scripts.PlayerContext
{
    public class Player : MonoBehaviour, IDamageable, ITransformable
    {
        private TrackerUnits _trackerUnits;
        private Health _health;
        private Coroutine _shoot;
    
        public Transform Transform { get; private set; }

        [Inject]
        public void Construct(TrackerUnits trackerUnits)
        {
            _trackerUnits = trackerUnits;
        }
        
        private void Awake()
        {
            Transform = GetComponent<Transform>();
        }

        private void Start()
        {
            StartCoroutine(StartTrackerUnits());
        }

        public void TakeDamage(int damage)
        {
            _health.Increase(damage);
        }

        private IEnumerator StartTrackerUnits()
        {
            while (enabled)
            {
                yield return new WaitForSeconds(0.3f);
            
                _trackerUnits.FindNearestPosition(transform.position);
            }
        }
    }
}