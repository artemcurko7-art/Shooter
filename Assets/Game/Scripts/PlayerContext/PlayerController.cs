using DG.Tweening;
using Game.Scripts.Animation;
using Game.Scripts.PlayerContext.GameInput;
using UnityEngine;
using Zenject;

namespace Game.Scripts.PlayerContext
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _deceleration;
        [SerializeField] private float _smooth;
        
        private Mover _mover;
        private Rotation _rotation;
        private IInput _input;
        private CharacterController _characterController;
        private Animator _animator;
        private Vector3 _lastNonZeroDirection;
        private Vector3 _currentVelocity;
        private float _currentSpeed;
        
        [Inject]
        public void Construct(Mover mover, Rotation rotation, IInput input)
        {
            _mover = mover;
            _rotation = rotation;
            _input = input;
            
            _characterController = GetComponent<CharacterController>();
            _animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            Movement();
        }

        private void Movement()
        {
            _input.Update();

            Vector3 direction = new Vector3(_input.Horizontal, 0f, _input.Vertical).normalized;
            Vector3 targetVelocity = direction * _speed;

            float rate = direction.sqrMagnitude > 0.001f ? _acceleration : _deceleration;
            _currentVelocity = Vector3.MoveTowards(_currentVelocity, targetVelocity, rate * Time.deltaTime);

            _characterController.Move(_currentVelocity * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                AffectImpact();
            }
            
            if (Input.GetKeyDown(KeyCode.V))
            {
                AffectStrafe();
            }
            
            _rotation.Rotate(transform, Vector3.zero, _input.Horizontal, _input.Vertical, _smooth * Time.deltaTime);
            _animator.SetFloat(PlayerAnimationData.Params.Speed, direction.sqrMagnitude, 0.05f, Time.deltaTime);
        }
        
        private void AffectImpact()
        {
            transform.DOPunchScale(new Vector3(0.15f, -0.2f, 0.15f), 0.16f, vibrato: 1, elasticity: 0.5f);
        }

        private void AffectStrafe()
        {
            transform.DOPunchScale(new Vector3(0.1f, -0.15f, 0.1f), 0.12f, 1, 0);
        }
    }
}