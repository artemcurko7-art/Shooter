using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Common.Classes;
using Game.Scripts.Common.Interfaces;
using Game.Scripts.Damagable;
using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.MV.StatContext;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.PlayerContext;
using Game.Scripts.UnitContext.FSM;
using Game.Scripts.UnitContext.FSM.Attacker;
using Game.Scripts.UnitContext.FSM.Death;
using Game.Scripts.UnitContext.FSM.Follower;
using Game.Scripts.UnitContext.Repository;
using Game.Scripts.UnitContext.Type;
using UnityEngine;
using Zenject;

namespace Game.Scripts.UnitContext
{
    [RequireComponent(typeof(CharacterController), typeof(Animator))]
    public class Unit : PhysicalBody<Unit>, IAttacker, IDamageable, ITransformable
    {
        [SerializeField] private Transform _shootPoint;
        
        private IUnitRandomAttackerService _attackerService; 
        private ITransformable _transformable;
        private CharacterController _characterController;
        private Renderer _renderer;
        private Material _material;

        public UnitType Type { get; private set; }
        protected State State { get; private set; }
        protected Animator Animator { get; private set; }
    
        public event Action<Unit> Released;
        public event Action<Unit> Dead;

        public IReadOnlyDictionary<StatType, Stat> Stats { get; private set; }
        public Transform Shoot => _shootPoint;
        public Transform Transform => transform;

        [Inject]
        public void Construct(IUnitRandomAttackerService attackerService, ITransformable transformable)
        {
            _attackerService = attackerService;
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

        public void Initialize(UnitType type, IReadOnlyDictionary<StatType, Stat> stats)
        {
            Type = type;
            Stats = stats;
            
            State = new State();
            
            State.Add(new UnitStateFollower(
                State, _characterController,
                transform,
                _transformable.Transform,
                Stats[StatType.Speed].Value,
                Stats[StatType.Distance].Value));
            
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
            Stats[StatType.Health].Decrease(damage);
            
            if (Stats[StatType.Health].Value <= 0)
            {
                State.Set<StateDeath>();
                _characterController.enabled = false;
                Dead?.Invoke(this);
                StartCoroutine(StartLife());
            }
        }
        
        public virtual void Attack()
        {
            var attacker = _attackerService.GetRandom(this, StateType.Attacker);
            attacker.Attack(_shootPoint);
        }
        
        protected virtual StateAttacker BindStateAttacker()
        {
            return new UnitStateAttacker(
                State,
                Animator,
                transform,
                _transformable.Transform,
                Stats[StatType.Distance].Value);
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

            foreach (var stat in Stats.Values)
                stat.SetDefaultValue();
        }
    }
}