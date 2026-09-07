using System.Collections;
using Game.Scripts.Damagable;
using Game.Scripts.MV.StatContext;
using Game.Scripts.WeaponContext;
using UnityEngine;
using Zenject;

namespace Game.Scripts.PlayerContext
{
    public class Player : MonoBehaviour, IDamagable, ITransformable
    {
        [SerializeField] private Bullet _bullet;
        [SerializeField] private Transform _pointBullet;
        [SerializeField] private LayerMask _unit;
        [SerializeField] private float _radius;
        
        private Health _health;
        private TrackerUnits _trackerUnits;
        private Coroutine _shoot;
        private Vector3 _tracker;
    
        public Transform Transform { get; private set; }

        [Inject]
        public void Construct(TrackerUnits trackerUnits)
        {
            _trackerUnits = trackerUnits;
        }
    
        private void Awake()
        {
            Transform = GetComponent<Transform>();
            StartCoroutine(Track());
        }

        private void Update()
        {
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

            // if (Input.GetKeyDown(KeyCode.Mouse0))
            // {
            //     _animator.SetTrigger(PlayerAnimationData.Params.Attack);
            // }
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