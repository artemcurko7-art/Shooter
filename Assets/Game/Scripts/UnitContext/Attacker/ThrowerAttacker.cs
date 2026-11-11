using Game.Scripts.PoolMono;
using Game.Scripts.PoolMono.Repository.Projectile;
using Game.Scripts.ProjectileContext;
using Game.Scripts.ProjectileContext.Type;
using Game.Scripts.UnitContext.AttributeContext;
using Game.Scripts.UnitContext.Type;
using UnityEngine;

namespace Game.Scripts.UnitContext.Attacker
{
    public class ThrowerAttacker : IUnitAttacker
    {
        private readonly ProjectilePool _projectilePool;
        
        public ThrowerAttacker(ProjectilePool projectilePool)
        {
            _projectilePool = projectilePool;
        }
        
        public void Attack(Transform current)
        {
            // var obj = GameObject.Instantiate(_projectile, current.transform.position, Quaternion.identity);
            // obj.Initialize(current.transform.position, current.forward, 5, damage, 3);
        }
    }
}