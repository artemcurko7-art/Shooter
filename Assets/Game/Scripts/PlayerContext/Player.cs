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
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour, IDamagable, ITransformable
    {
        [SerializeField] private Bullet _bullet;
        [SerializeField] private Transform _pointBullet;
        [SerializeField] private LayerMask _unit;
        [SerializeField] private float _speed;
        [SerializeField] private float _smooth;
        [SerializeField] private float _radius;
        
        private Health _health;
        private Mover _mover;
        private Rotation _rotation;
        private TrackerUnits _trackerUnits;
        private IInput _input;
        private Rigidbody _rigidbody;
        private Animator _animator;
        private Coroutine _shoot;
        private Vector3 _tracker;
    
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
            _rigidbody = GetComponent<Rigidbody>();
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

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _animator.SetTrigger(PlayerAnimationData.Params.Attack);
            }
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
            
            _mover.Move(_rigidbody, _input.Horizontal, _input.Vertical, _speed);
            _rotation.Rotate(transform, _tracker, _input.Horizontal, _input.Vertical, _smooth * Time.fixedDeltaTime);
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