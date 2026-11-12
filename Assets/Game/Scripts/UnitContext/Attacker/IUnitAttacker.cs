using Game.Scripts.HitImpacted;
using Game.Scripts.UnitContext.Type;
using UnityEngine;

namespace Game.Scripts.UnitContext.Attacker
{
    public interface IUnitAttacker : IImpactReceiver
    {
        void Attack(Transform current);
    }
}