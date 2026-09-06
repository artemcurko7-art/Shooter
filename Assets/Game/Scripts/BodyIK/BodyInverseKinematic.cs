using Game.Scripts.Configs;
using Game.Scripts.Service.Weapon;
using UnityEngine;
using Zenject;

namespace Game.Scripts.BodyIK
{
    public class BodyInverseKinematic : MonoBehaviour
    {
        private const int Weight = 1;

        private WeaponService _weaponService;
        private Animator _animator;
    
        [Inject]
        public void Construct(WeaponService weaponService, BodyInverseKinematicData data)
        {
            _weaponService = weaponService;
            _animator = GetComponent<Animator>();
        }

        private void OnAnimatorIK(int layerIndex)
        {
            SetIK(AvatarIKGoal.LeftHand, _weaponService.View.LeftHandGrip.position, _weaponService.View.LeftHandGrip.rotation);
            SetIK(AvatarIKGoal.RightHand, _weaponService.View.RightHandGrip.position, _weaponService.View.RightHandGrip.rotation);
        }

        private void SetIK(AvatarIKGoal type, Vector3 position, Quaternion rotation)
        {
            _animator.SetIKPositionWeight(type, Weight);
            _animator.SetIKRotationWeight(type, Weight);
            _animator.SetIKPosition(type, position);
            _animator.SetIKRotation(type, rotation);
        }
    }
}