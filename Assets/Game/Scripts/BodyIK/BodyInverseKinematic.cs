using System;
using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.Service.Weapon;
using Game.Scripts.WeaponContext;
using UnityEngine;
using Zenject;

namespace Game.Scripts.BodyIK
{
    [RequireComponent(typeof(Animator))]
    public class BodyInverseKinematic : MonoBehaviour
    {
        private const int Weight = 1;
        
        private WeaponView _weaponView;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnAnimatorIK(int layerIndex)
        {
            _weaponView = GetComponentInChildren<WeaponView>(); // временно
            
            SetIK(AvatarIKGoal.LeftHand, _weaponView.LeftHandGrip.position, _weaponView.LeftHandGrip.rotation);
            SetIK(AvatarIKGoal.RightHand, _weaponView.RightHandGrip.position, _weaponView.RightHandGrip.rotation);
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