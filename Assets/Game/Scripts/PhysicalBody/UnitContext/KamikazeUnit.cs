using System;
using Game.Scripts.PhysicalBody.UnitContext.Attacker;
using Game.Scripts.PhysicalBody.UnitContext.FSM;
using Game.Scripts.PhysicalBody.UnitContext.FSM.Follower;
using Game.Scripts.PhysicalBody.UnitContext.Type;
using UnityEngine;

namespace Game.Scripts.PhysicalBody.UnitContext
{
    public class KamikazeUnit : Unit
    {
        private const float Weight = 1;
        
        [SerializeField] private Transform _handL;
        [SerializeField] private Transform _handR;
        
        private State _state;

        private void Update()
        {
            _state.Update();
        }

        public override void Initialize(UnitType type, IUnitAttacker attacker, int health, int damage, float speed, float distance)
        {
            base.Initialize(type, attacker, health, damage, speed, distance);
            
            _state = new State();
            
            _state.Add(new KamikazeUnitStateFollower(_state, CharacterController, transform, Transformable.Transform, speed, distance));
            _state.Add(new KamikazeUnitStateAttacker(_state, Animator));
            
            _state.Set<KamikazeUnitStateFollower>();
        }
        
        public override void Attack()
        {
            base.Attack();
            
            Destroy(gameObject);
        }

        private void OnAnimatorIK(int layerIndex)
        {
            SetIK(AvatarIKGoal.LeftHand, _handL.position, _handL.rotation);
            SetIK(AvatarIKGoal.RightHand, _handR.position, _handR.rotation);
        }
        
        private void SetIK(AvatarIKGoal type, Vector3 position, Quaternion rotation)
        {
            Animator.SetIKPositionWeight(type, Weight);
            Animator.SetIKRotationWeight(type, Weight);
            Animator.SetIKPosition(type, position);
            Animator.SetIKRotation(type, rotation);
        }
    }
}