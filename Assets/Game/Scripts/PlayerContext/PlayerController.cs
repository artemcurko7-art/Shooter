using DG.Tweening;
using Game.Scripts.Animation;
using Game.Scripts.PhysicalBody.UnitContext;
using Game.Scripts.PlayerContext.GameInput;
using Game.Scripts.Service.PhysicalBody;
using Game.Scripts.Service.Weapon;
using UnityEngine;
using Zenject;

namespace Game.Scripts.PlayerContext
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Unit _unit;
        [SerializeField] private float _speed;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _deceleration;
        [SerializeField] private float _smooth;

        private WeaponService _weaponService;
        private TrackerUnits _trackerUnits;
        private Mover _mover;
        private Rotation _rotation;
        private IInput _input;
        private CharacterController _characterController;
        private Animator _animator;
        private Vector3 _lastNonZeroDirection;
        private Vector3 _currentVelocity;
        private float _currentSpeed;
        
        [Inject]
        public void Construct(WeaponService weaponService, TrackerUnits trackerUnits, Mover mover, Rotation rotation, IInput input)
        {
            _weaponService = weaponService;
            _trackerUnits = trackerUnits;
            _mover = mover;
            _rotation = rotation;
            _input = input;
            
            _characterController = GetComponent<CharacterController>();
            _animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            _input.Update();
            _mover.Move(_characterController, _acceleration, _deceleration, _speed);
            //_rotation.Rotate(transform, _unit.transform.position, _input.Horizontal, _input.Vertical, _smooth * Time.deltaTime);
            //_rotation.Rotate(transform, _weaponService.View.transform.position, _input.Horizontal, _input.Vertical, _smooth * Time.deltaTime);
            _rotation.Rotate(transform, _trackerUnits.Direction, _input.Horizontal, _input.Vertical, _smooth * Time.deltaTime);
            _animator.SetFloat(PlayerAnimationData.Params.Speed, _mover.Direction.sqrMagnitude, 0.05f, Time.deltaTime);
        }
        
        // private void AffectImpact()
        // {
        //     transform.DOPunchScale(new Vector3(0.15f, -0.2f, 0.15f), 0.16f, vibrato: 1, elasticity: 0.5f);
        // }
        //
        // private void AffectStrafe()
        // {
        //     transform.DOPunchScale(new Vector3(0.1f, -0.15f, 0.1f), 0.12f, 1, 0);
        // }
    }
}