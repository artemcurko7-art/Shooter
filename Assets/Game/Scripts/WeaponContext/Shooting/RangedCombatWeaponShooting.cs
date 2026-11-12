using System;
using Cysharp.Threading.Tasks;
using System.Threading;
using Game.Scripts.Common.Interfaces;
using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.HitImpacted;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.PlayerContext;
using Game.Scripts.PoolMono;
using Game.Scripts.WeaponContext.Attribute;
using Game.Scripts.WeaponContext.Type;
using UnityEngine;

namespace Game.Scripts.WeaponContext.Shooting
{
    [WeaponShootingType(ShootingType.RangedCombat)]
    public class RangedCombatWeaponShooting : WeaponShooting, IWeaponShooting
    {
        private CriticalDamageResolver _criticalDamageResolver;
        private CancellationTokenSource _cancellationTokenSource;
        private int _countShoot;
        private bool _canShoot = true;

        public RangedCombatWeaponShooting(
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
            _criticalDamageResolver = new CriticalDamageResolver(attacker.Stats[StatType.CriticalChance].Value);
            
            Debug.Log("Ranged combat");
        }
        
        private async UniTaskVoid StartCooldown(IAttacker attacker, CancellationToken token)
        {
            ProjectilePool.SetPrefab(Config.Projectile);
            
            while (token.IsCancellationRequested == false)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(Config.CooldownShoot), cancellationToken: token);
        
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
                    var projectile = ProjectilePool.Get();
                    
                    float currentDamage = attacker.Stats[StatType.Damage].Value * 2.5f;
                    bool isCritical = _criticalDamageResolver.IsCritical();
                    
                    if (isCritical)
                    {
                        float calculation = currentDamage * (1f + attacker.Stats[StatType.CriticalDamage].Value / 100f);
                        currentDamage = calculation;
                    }
                    
                    projectile.Initialize(
                        Config.LayerMask,
                        new ShotContext(attacker, Mathf.CeilToInt(currentDamage), isCritical),
                        attacker.Shoot.position,
                        TrackerUnits.GetNearestPosition(attacker.Shoot.position),
                        Mathf.CeilToInt(currentDamage),
                        Config.Radius,
                        Config.Speed);
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
            
            await UniTask.Delay(TimeSpan.FromSeconds(Config.CooldownReload), cancellationToken: token);

            _canShoot = true;
        }
    }
}