using System;
using Game.Scripts.Damagable;
using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.MV.StatContext;
using Game.Scripts.PlayerContext.Death;
using UnityEngine;
using Zenject;

namespace Game.Scripts.PlayerContext
{
    public class Player : MonoBehaviour, IDamageable, ITransformable, IDeath
    {
        private Health _health;

        public event Action Dead;
        
        public Transform Transform { get; private set; }

        [Inject]
        public void Construct(Health health)
        {
            _health = health;
        }
        
        private void Awake()
        {
            Transform = GetComponent<Transform>();
        }

        public void TakeDamage(int damage)
        {
            _health.Decrease(damage);

            if (_health.Value <= 0)
                Dead?.Invoke();
        }
    }
}