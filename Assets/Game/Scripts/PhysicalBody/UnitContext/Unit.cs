using System;
using Game.Scripts.Damagable;
using Game.Scripts.PhysicalBody.UnitContext.Attacker;
using Game.Scripts.PlayerContext;
using UnityEngine;
using Zenject;

namespace Game.Scripts.PhysicalBody.UnitContext
{
    [RequireComponent(typeof(CharacterController), typeof(Animator))]
    public abstract class Unit : PhysicalBody<Unit>, IDamageable, ITransformable
    {
        private int _health;
        private int _damage;
        private float _distance;
        
        protected IUnitAttacker Attacker { get; private set; }
        protected ITransformable Transformable { get; private set; }
        protected CharacterController CharacterController { get; private set; }
        protected Animator Animator { get; private set; }
    
        public event Action<Unit> Disabled;

        public Transform Transform { get; }

        [Inject]
        public void Construct(ITransformable transformable)
        {
            Transformable = transformable;
            CharacterController = GetComponent<CharacterController>();
            Animator = GetComponent<Animator>();
        }

        public virtual void Initialize(IUnitAttacker attacker, int health, int damage, float speed, float distance)
        {
            Attacker = attacker;
            _health = health;
            _damage = damage;
            _distance = distance;
        }
        
        public void TakeDamage(int damage)
        {
            _health -= damage;
            
            if (_health <= 0)
                Disabled?.Invoke(this);
        }
        
        public abstract void Attack();
    }
}