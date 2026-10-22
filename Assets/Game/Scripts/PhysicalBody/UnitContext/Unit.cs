using System;
using System.Collections;
using Game.Scripts.Damagable;
using Game.Scripts.PhysicalBody.UnitContext.Attacker;
using Game.Scripts.PhysicalBody.UnitContext.FSM;
using Game.Scripts.PhysicalBody.UnitContext.FSM.Attacker;
using Game.Scripts.PhysicalBody.UnitContext.FSM.Death;
using Game.Scripts.PhysicalBody.UnitContext.FSM.Follower;
using Game.Scripts.PhysicalBody.UnitContext.Type;
using Game.Scripts.PlayerContext;
using UnityEngine;
using Zenject;

namespace Game.Scripts.PhysicalBody.UnitContext
{
    [RequireComponent(typeof(CharacterController), typeof(Animator))]
    public class Unit : PhysicalBody<Unit>, IDamageable, ITransformable
    {
        private IUnitAttacker _attacker;
        private Renderer _renderer;
        private Material _material;
        private int _health;
        private int _damage;
        private float _speed;
        private float _distance;

        public UnitType Type { get; private set; }
        protected State State { get; private set; }
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
            _renderer = GetComponentInChildren<Renderer>();
            _material = _renderer.material;
        }

        private void Update()
        {
            State.Update();
        }

        public void Initialize(UnitType type, IUnitAttacker attacker, int health, int damage, float speed, float distance)
        {
            Type = type;
            _attacker = attacker;
            _health = health;
            _damage = damage;
            _speed = speed;
            _distance = distance;
            
            State = new State();
            
            State.Add(new UnitStateFollower(State, CharacterController, transform, Transformable.Transform, _speed, _distance));
            State.Add(BindStateAttacker());
            State.Add(BindStateDeath());
            
            State.Set<StateFollower>();
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
            {
                State.Set<StateDeath>();
                StartCoroutine(StartLife());
            }
        }
        
        public virtual void Attack()
        {
            _attacker.Attack(transform, _damage);
        }
        
        protected virtual StateAttacker BindStateAttacker()
        {
            return new UnitStateAttacker(
                State,
                Animator,
                transform,
                Transformable.Transform,
                _distance);
        }

        protected virtual StateDeath BindStateDeath()
        {
            return new UnitStateDeath(
                State,
                Animator);
        }
        
        private IEnumerator StartLife()
        {
            yield return new WaitForSeconds(5);
            
            Released?.Invoke(this);
        }
    }
}