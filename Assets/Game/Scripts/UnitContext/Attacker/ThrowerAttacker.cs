using System;
using Game.Scripts.Common.Interfaces;
using Game.Scripts.HitImpacted;
using Game.Scripts.MV.StatContext;
using Game.Scripts.PoolMono;
using Game.Scripts.ProjectileContext;
using Game.Scripts.WeaponContext;
using UnityEngine;

namespace Game.Scripts.UnitContext.Attacker
{
    public class ThrowerAttacker : IUnitAttacker
    {
        private readonly LayerMask _layerMask;
        private readonly IAttacker _attacker;
        private readonly ProjectilePool _projectilePool;
        private readonly Stat _damage;
        private readonly Stat _speed;
        private readonly Stat _radius;
        private Projectile _projectile;
        
        public event Action<HitContext> HitImpacted;
        
        public ThrowerAttacker(LayerMask layerMask, IAttacker attacker, ProjectilePool projectilePool, Stat damage, Stat speed, Stat radius)
        {
            _layerMask = layerMask;
            _attacker = attacker;
            _projectilePool = projectilePool;
            _damage = damage;
            _speed = speed;
            _radius = radius;
        }
        
        public void Attack(Transform current)
        {
            _projectile = _projectilePool.Get();
            _projectile.HitImpacted += OnHitImpacted;
            
            _projectile.Initialize(
                _layerMask,
                new ShotContext(_attacker, _damage.Value, false),
                current.position,
                current.forward,
                _damage.Value,
                _radius.Value,
                _speed.Value);
            
            Debug.Log("Thrower Attacker");
        }

        private void OnHitImpacted(HitContext hit)
        {
            HitImpacted?.Invoke(hit);
            _projectile.HitImpacted -= OnHitImpacted;
        }
    }
}