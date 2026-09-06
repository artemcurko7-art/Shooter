using System.Collections;
using Game.Scripts.Animation;
using Game.Scripts.Damagable;
using Game.Scripts.MV.StatContext;
using Game.Scripts.PlayerContext.GameInput;
using Game.Scripts.WeaponContext;
using UnityEngine;
using Zenject;

namespace Game.Scripts.PlayerContext
{
    [RequireComponent(typeof(CharacterController))]
    public class Player : MonoBehaviour, IDamagable, ITransformable
    {
        [SerializeField] private Bullet _bullet;
        [SerializeField] private Transform _pointBullet;
        [SerializeField] private LayerMask _unit;
        [SerializeField] private float _speed;
        [SerializeField] private float _smooth;
        [SerializeField] private float _damping;
        [SerializeField] private float _radius;
        
        private Health _health;
        private Mover _mover;
        private Rotation _rotation;
        private TrackerUnits _trackerUnits;
        private IInput _input;
        private CharacterController _characterController;
        private Animator _animator;
        private Coroutine _shoot;
        private Vector3 _tracker;
        private float _currentHorizontal;
        private float _currentVertical;
        private float _currentSpeed;
    
        public Transform Transform { get; private set; }

        [Inject]
        public void Construct(
            Mover mover,
            Rotation rotation,
            TrackerUnits trackerUnits, 
            IInput input)
        {
            _mover = mover;
            _rotation = rotation;
            _trackerUnits = trackerUnits;
            _input = input;
        }
    
        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _animator = GetComponentInChildren<Animator>();
            Transform = GetComponent<Transform>();
            StartCoroutine(Track());
        }

        private void Update()
        {
            _input.Update();
        
            if (_trackerUnits.IsTracker == false)
            {
                if (_shoot != null)
                {
                    StopCoroutine(_shoot);
                    _shoot = null;
                }
            }
            else
            {
                // if (_shoot == null)
                //     _shoot = StartCoroutine(Shoot());
            }

            HandleMovement();
            
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _animator.SetTrigger(PlayerAnimationData.Params.Attack);
            }
            
            Debug.Log($"Current speed: {_currentSpeed}");
            //Vector3 inputDirection = (transform.forward * _currentVertical + transform.right * _currentHorizontal).normalized;
            //Debug.Log($"Input direction: {inputDirection}");
            //_animator.SetFloat(PlayerAnimationData.Params.Speed, inputDirection.magnitude);
        }

        private void FixedUpdate()
        {
            // if (_input.Horizontal != 0 || _input.Vertical != 0)
            // {
            //     _animator.SetBool(PlayerAnimationData.Params.IsWalk, true);
            // }
            // else
            // {
            //     _animator.SetBool(PlayerAnimationData.Params.IsWalk, false);
            // }
            
            //_mover.Move(_rigidbody, _currentHorizontal, _currentVertical, _currentSpeed);
            //_rotation.Rotate(transform, _tracker, _input.Horizontal, _input.Vertical, _smooth * Time.fixedDeltaTime);
        }
        
        private void HandleMovement()
        {
            Vector3 inputVector = new Vector3(_input.Horizontal, 0f, _input.Vertical);
            Vector3 moveDirection = Vector3.ClampMagnitude(inputVector, 1f);
            Debug.Log($"Move direction: {moveDirection}");

            float targetSpeed = moveDirection.magnitude;

            if (targetSpeed > 0.01f)
            {
                _rotation.Rotate(transform, _tracker, _input.Horizontal, _input.Vertical, _smooth * Time.deltaTime);
                //_mover.Move(_rigidbody, _currentHorizontal, _currentVertical, _currentSpeed);
                _characterController.Move(moveDirection * _speed * Time.deltaTime);
            }
            
            _animator.SetFloat(PlayerAnimationData.Params.Speed, targetSpeed, _damping, Time.deltaTime);
        }
    
        public void TakeDamage(int damage)
        {
            _health.Increase(damage);
        }

        // private IEnumerator Shoot()
        // {
        //     while (enabled)
        //     {
        //         yield return new WaitForSeconds(2f);
        //         
        //         _weapon.Shoot(_pointBullet, _bullet);
        //
        //         yield return null;
        //     }
        // }

        private IEnumerator Track()
        {
            while (enabled)
            {
                yield return new WaitForSeconds(0.5f);
            
                _tracker = _trackerUnits.GetNearestPosition(transform.position, _radius, _unit);
    
                yield return null;
            }
        }
    }
}