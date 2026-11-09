using Game.Scripts.ProjectileContext;
using Game.Scripts.UnitContext.Type;
using UnityEngine;

namespace Game.Scripts.UnitContext.Attacker
{
    public class ThrowerAttacker : IUnitAttacker
    {
        private readonly Projectile _projectile;
        
        public ThrowerAttacker(Projectile projectile)
        {
            Type = UnitAttackerType.Thrower;
            _projectile = projectile;
        }
        
        public UnitAttackerType Type { get; }
        
        public void Attack(Transform current, int damage, float distance)
        {
            // var obj = GameObject.Instantiate(_projectile, current.transform.position, Quaternion.identity);
            // obj.Initialize(current.transform.position, current.forward, 5, damage, 3);
        }
    }
}