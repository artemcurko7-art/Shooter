using Cysharp.Threading.Tasks;
using System.Threading;
using Game.Scripts.PlayerContext;
using Game.Scripts.WeaponContext.Type;
using UnityEngine;

namespace Game.Scripts.WeaponContext.Shooting
{
    public class Single : IWeaponShooting
    {
        private const int SecondInMilliseconds = 1000;
        private readonly TrackerUnits _trackerUnits;
        private readonly float _cooldown;
        private CancellationTokenSource _cancellationTokenSource;
        private bool _canShoot;
        
        public Single(TrackerUnits trackerUnits, float cooldown)
        {
            Type = ShootingType.Single;
            _trackerUnits = trackerUnits;
            _cooldown = cooldown;
        }
        
        public ShootingType Type { get; }
        
        public void Subscribe()
        {
            // добавить
        }

        public void Unsubscribe()
        {
            _cancellationTokenSource.Cancel();
        }
        
        public void Shoot(Transform transform, Bullet bullet)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            StartCooldown(transform, bullet, _cancellationTokenSource.Token).Forget();
            
            Debug.Log("Single");
        }
        
        private async UniTaskVoid StartCooldown(Transform transform, Bullet bullet, CancellationToken token)
        {
            while (token.IsCancellationRequested == false)
            {
                float calculationCooldown = _cooldown * SecondInMilliseconds;
                
                await UniTask.Delay((int)calculationCooldown, cancellationToken: token);
        
                if (token.IsCancellationRequested)
                    return;

                if (_trackerUnits.IsTracker == false)
                    return;
                
                var obj = GameObject.Instantiate(bullet, transform.position, Quaternion.identity);
                //obj.SetDirection(_trackerUnits.Direction);
                obj.SetDirection(transform.forward);
            }
        }
    }
}