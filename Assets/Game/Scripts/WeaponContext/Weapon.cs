using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.WeaponContext.Data;
using Game.Scripts.WeaponContext.Shooting;
using UnityEngine;

namespace Game.Scripts.WeaponContext
{
    public class Weapon
    {
        public Weapon(IWeaponShooting shooting, IAttacker attacker)
        {
            Shooting = shooting;
            
            shooting.StartShooting(attacker);
        }
        
        public IWeaponShooting Shooting { get; private set; }
    }
}