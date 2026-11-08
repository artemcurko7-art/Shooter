using System;
using Game.Scripts.Damagable;
using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.MV.StatContext;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.PlayerContext.Death;
using UnityEngine;
using Zenject;

namespace Game.Scripts.PlayerContext
{
    public class Player : MonoBehaviour, IDamageable, ITransformable, IDeath
    {
        private ICharacterHealthResolver _healthResolver;

        public event Action Dead;
        
        public Transform Transform { get; private set; }

        [Inject]
        public void Construct(ICharacterHealthResolver healthResolver)
        {
            _healthResolver = healthResolver;
        }
        
        private void Awake()
        {
            Transform = GetComponent<Transform>();
        }

        public void TakeDamage(int damage)
        {
            _healthResolver.Health.Decrease(damage);
            
            if (_healthResolver.Health.Value <= 0)
                Dead?.Invoke();
        }
    }
}