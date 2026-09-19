using System;
using Game.Scripts.Damagable;
using Game.Scripts.PhysicalBody.UnitContext.Attacker;
using Game.Scripts.PhysicalBody.UnitContext.Type;
using Game.Scripts.PlayerContext;
using UnityEngine;
using Zenject;

namespace Game.Scripts.PhysicalBody.UnitContext
{
    [RequireComponent(typeof(CharacterController), typeof(Animator))]
    public abstract class Unit : PhysicalBody<Unit>, IDamageable, ITransformable
    {
        private IUnitAttacker _attacker;
        private Renderer _renderer;
        private Material _material;
        private int _health;
        private int _damage;
        private float _distance;

        public UnitType Type { get; private set; }
        protected ITransformable Transformable { get; private set; }
        protected CharacterController CharacterController { get; private set; }
        protected Animator Animator { get; private set; }
    
        public event Action<Unit> Released;

        public Transform Transform { get; }

        [Inject]
        public void Construct(ITransformable transformable)
        {
            Transformable = transformable;
            CharacterController = GetComponent<CharacterController>();
            Animator = GetComponent<Animator>();
            //_renderer = GetComponent<Renderer>();
            //_material = _renderer.material;
        }

        public virtual void Initialize(UnitType type, IUnitAttacker attacker, int health, int damage, float speed, float distance)
        {
            Type = type;
            _attacker = attacker;
            _health = health;
            _damage = damage;
            _distance = distance;
        }

        public override void ResetSettings()
        {
            base.ResetSettings();
            
            _renderer.material = _material;
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
            
            if (_health <= 0)
                Released?.Invoke(this);
        }
        
        public virtual void Attack()
        {
            _attacker.Attack(transform, _damage);
        }
    }
}