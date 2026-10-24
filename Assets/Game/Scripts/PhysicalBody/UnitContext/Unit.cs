using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Damagable;
using Game.Scripts.MV.StatContext;
using Game.Scripts.MV.StatContext.Type;
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
        private IReadOnlyDictionary<StatType, Stat> _stats;
        private IUnitAttacker _attacker;
        private ITransformable _transformable;
        private CharacterController _characterController;
        private Renderer _renderer;
        private Material _material;

        public UnitType Type { get; private set; }
        protected State State { get; private set; }
        protected Animator Animator { get; private set; }
    
        public event Action<Unit> Released;
        public event Action<Unit> Dead;

        public Transform Transform { get; }

        [Inject]
        public void Construct(ITransformable transformable)
        {
            _transformable = transformable;
            _characterController = GetComponent<CharacterController>();
            Animator = GetComponent<Animator>();
            _renderer = GetComponentInChildren<Renderer>();
            _material = _renderer.material;
        }

        private void Update()
        {
            State.Update();
        }

        public void Initialize(UnitType type, IUnitAttacker attacker, IReadOnlyDictionary<StatType, Stat> stats)
        {
            Type = type;
            _attacker = attacker;
            _stats = stats;
            
            State = new State();
            
            State.Add(new UnitStateFollower(
                State, _characterController,
                transform,
                _transformable.Transform,
                _stats[StatType.Speed].Value,
                _stats[StatType.Distance].Value));
            
            State.Add(BindStateAttacker());
            State.Add(BindStateDeath());
            
            State.Set<StateFollower>();
        }

        public override void ResetSettings()
        {
            base.ResetSettings();
            
            _renderer.material = _material;
            _characterController.enabled = true;
        }

        public void TakeDamage(int damage)
        {
            _stats[StatType.Health].Decrease(damage);
            
            if (_stats[StatType.Health].Value <= 0)
            {
                State.Set<StateDeath>();
                _characterController.enabled = false;
                Dead?.Invoke(this);
                StartCoroutine(StartLife());
            }
        }
        
        public virtual void Attack()
        {
            _attacker.Attack(transform, _stats[StatType.Damage].Value, _stats[StatType.Distance].Value);
        }
        
        protected virtual StateAttacker BindStateAttacker()
        {
            return new UnitStateAttacker(
                State,
                Animator,
                transform,
                _transformable.Transform,
                _stats[StatType.Distance].Value);
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

            foreach (var stat in _stats.Values)
                stat.SetDefaultValue();
        }
    }
}