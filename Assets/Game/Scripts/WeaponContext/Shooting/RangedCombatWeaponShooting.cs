using System;
using Cysharp.Threading.Tasks;
using System.Threading;
using Game.Scripts.PlayerContext;
using Game.Scripts.PoolMono;
using Game.Scripts.WeaponContext.Type;
using UnityEngine;

namespace Game.Scripts.WeaponContext.Shooting
{
    public class RangedCombatWeaponShooting : WeaponShooting, IWeaponShooting
    {
        private const int SecondInMilliseconds = 1000;
        private readonly Transform _transform;
        private CancellationTokenSource _cancellationTokenSource;
        private int _countShoot;
        private bool _canShoot = true;

        public RangedCombatWeaponShooting(IWeaponShootingConfig config, TrackerUnits trackerUnits, ProjectilePool projectilePool, Transform transform)
            : base(config, trackerUnits, projectilePool)
        {
            _transform = transform;
        }

        public event Action Attacked;
        
        public void Subscribe()
        {
            // добавить
        }

        public void Unsubscribe()
        {
            _cancellationTokenSource.Cancel();
        }
        
        public void StartShooting()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            StartCooldown(_cancellationTokenSource.Token).Forget();
            
            Debug.Log("Ranged combat");
        }
        
        protected override ShootingType GetType()
        {
            return ShootingType.RangedCombat;
        }
        
        private async UniTaskVoid StartCooldown(CancellationToken token)
        {
            ProjectilePool.SetPrefab(Config.Projectile);
            
            while (token.IsCancellationRequested == false)
            {
                float calculationCooldownShoot = Config.CooldownShoot * SecondInMilliseconds;
                
                await UniTask.Delay((int)calculationCooldownShoot, cancellationToken: token);
        
                if (token.IsCancellationRequested)
                    return;

                if (TrackerUnits.IsTracker == false)
                {
                    _countShoot = 0;
                    continue;
                }

                if (_canShoot)
                {
                    Attacked?.Invoke();
                    var obj = ProjectilePool.Get();
                    obj.Initialize(_transform.position, TrackerUnits.Direction, Config.Radius, Config.Damage, Config.Speed);
                    _countShoot++;
                }

                if (Config.MaxCountShoot == _countShoot)
                {
                    _countShoot = 0;
                    Reload(token).Forget();
                }
            }
        }

        private async UniTaskVoid Reload(CancellationToken token)
        {
            _canShoot = false;
            
            float calculationCooldownReload = Config.CooldownReload * SecondInMilliseconds;
            
            await UniTask.Delay((int)calculationCooldownReload, cancellationToken: token);

            _canShoot = true;
        }
    }
}