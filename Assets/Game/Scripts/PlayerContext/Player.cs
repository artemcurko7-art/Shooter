using System.Collections;
using Game.Scripts.Damagable;
using Game.Scripts.MV.Stat;
using Game.Scripts.PlayerContext.Input;
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

        private Weapon _weapon;
        private Health _health;
        private Mover _mover;
        private Rotation _rotation;
        private TrackerUnits _trackerUnits;
        private IInput _input;
        private Rigidbody _rigidbody;
        private Coroutine _shoot;
        private Vector3 _tracker;
    
        public Transform Transform { get; private set; }

        [Inject]
        public void Construct(
            Weapon weapon, 
            Health health, 
            Mover mover,
            Rotation rotation,
            TrackerUnits trackerUnits, 
            IInput input)
        {
            _weapon = weapon;
            _health = health;
            _mover = mover;
            _rotation = rotation;
            _trackerUnits = trackerUnits;
            _input = input;
        }
    
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
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
                if (_shoot == null)
                    _shoot = StartCoroutine(Shoot());
            }
        }

        private void FixedUpdate()
        {
            _mover.Move(_rigidbody, _input.Horiontal, _input.Vertical, _speed);
            _rotation.Rotate(transform, _tracker, _input.Horiontal, _input.Vertical, _smooth * Time.fixedDeltaTime);
        }
    
        public void TakeDamage(int damage)
        {
            _health.TakeDamage(damage);
        }

        private IEnumerator Shoot()
        {
            while (enabled)
            {
                yield return new WaitForSeconds(2f);
                
                _weapon.Shoot(_pointBullet, _bullet);
        
                yield return null;
            }
        }

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