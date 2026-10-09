using System;
using Cysharp.Threading.Tasks;
using System.Threading;
using Game.Scripts.Configs;
using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.PlayerContext;
using Game.Scripts.PoolMono;
using Game.Scripts.WeaponContext.Attribute;
using Game.Scripts.WeaponContext.Type;
using UnityEngine;
using Zenject;

namespace Game.Scripts.WeaponContext.Shooting
{
    [WeaponShootingType(ShootingType.Melee)]
    public class MeleeCombatWeaponShooting : WeaponShooting, IWeaponShooting
    {
        private const int SecondInMilliseconds = 1000;
        private CancellationTokenSource _cancellationTokenSource;
        private int _countShoot;
        private bool _canShoot = true;

        public MeleeCombatWeaponShooting(
            IWeaponShootingConfig config,
            TrackerUnits trackerUnits,
            ProjectilePool projectilePool) 
            : base(config, trackerUnits, projectilePool) { }
        
        public event Action<IAttacker> Attacked;

        public void Subscribe()
        {
            // добавить
        }

        public void Unsubscribe()
        {
            _cancellationTokenSource.Cancel();
        }
        
        public void StartShooting(IAttacker attacker)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            StartCooldown(attacker, _cancellationTokenSource.Token).Forget();
            
            Debug.Log("Melee combat");
        }
        
        protected override ShootingType GetType()
        {
            return ShootingType.RangedCombat;
        }
        
        private async UniTaskVoid StartCooldown(IAttacker attacker, CancellationToken token)
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
                    Attacked?.Invoke(attacker);
                    var obj = ProjectilePool.Get();
                    obj.Initialize(attacker, attacker.Transform.position, TrackerUnits.GetNearestPosition(attacker.Transform.position), Config.Radius, Config.Damage, Config.Speed);
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