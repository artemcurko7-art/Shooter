using Game.Scripts.PhysicalBody.UnitContext.FSM.Attacker;
using UnityEngine;

namespace Game.Scripts.PhysicalBody.UnitContext
{
    public class KamikazeUnit : Unit
    {
        private const float Weight = 1;
        
        [SerializeField] private Transform _handL;
        [SerializeField] private Transform _handR;
        
        public override void Attack()
        {
            base.Attack();
            
            Destroy(gameObject);
        }

        protected override StateAttacker BindStateAttacker()
        {
            return new KamikazeUnitStateAttacker(State, Animator);
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