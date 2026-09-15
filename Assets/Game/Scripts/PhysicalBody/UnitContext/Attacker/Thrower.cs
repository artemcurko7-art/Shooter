using Game.Scripts.PhysicalBody.UnitContext.Type;
using Game.Scripts.WeaponContext;
using UnityEngine;

namespace Game.Scripts.PhysicalBody.UnitContext.Attacker
{
    public class Thrower : IUnitAttacker
    {
        private readonly Bullet _bullet;
        
        public Thrower(Bullet bullet)
        {
            Type = UnitAttackerType.Thrower;
            _bullet = bullet;
        }
        
        public UnitAttackerType Type { get; }
        
        public void Attack(Transform current, int damage)
        {
            var obj = GameObject.Instantiate(_bullet, current.transform.position, Quaternion.identity);
            obj.Initialize(current.forward, 5, damage, 3);
        }
    }
}