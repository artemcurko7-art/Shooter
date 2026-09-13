using System;
using Game.Scripts.Damagable;
using Game.Scripts.PhysicalBody.UnitContext.Attacker;
using Game.Scripts.PhysicalBody.UnitContext.FSM;
using Game.Scripts.PhysicalBody.UnitContext.FSM.Follower;
using Game.Scripts.PlayerContext;
using Game.Scripts.WeaponContext;
using UnityEngine;
using Zenject;

namespace Game.Scripts.PhysicalBody.UnitContext
{
    public class ThrowerUnit : Unit
    {
        [SerializeField] private Bullet _bullet;
        [SerializeField] private Transform _point;
         
        private readonly Collider[] _colliders = new Collider[16];
        private LayerMask _layerMask;
        private State _state;

        private void Update()
        {
            _state.Update();
        }
    
        public override void Initialize(IUnitAttacker attacker, int health, int damage, float speed, float distance)
        {
            base.Initialize(attacker, health, damage, speed, distance);

            _state = new State();
            
            _state.Add(new ThrowerUnitStateFollower(_state, CharacterController, transform, Transformable.Transform, 3, 15));
            _state.Add(new ThrowerUnitStateAttacker(_state, Animator, transform, Transformable.Transform, 15));
            
            _state.Set<ThrowerUnitStateFollower>();
        }

        public override void Attack()
        {
            var obj = Instantiate(_bullet, _point.transform.position, Quaternion.identity);
            obj.Initialize(transform.forward, 5, 5, 3);
            
            Debug.Log("Thrower Hit");
        }
    }
}