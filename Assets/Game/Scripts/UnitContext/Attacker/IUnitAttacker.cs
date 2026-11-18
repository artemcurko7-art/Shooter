using Game.Scripts.HitImpacted;
using UnityEngine;

namespace Game.Scripts.UnitContext.Attacker
{
    public interface IUnitAttacker : IImpactReceiver
    {
        void Attack(Transform current);
    }
}