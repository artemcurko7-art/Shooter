using Game.Scripts.Equipment.CharacterContext;
using UnityEngine;

namespace Game.Scripts.HitImpacted
{
    public class HitContext
    {
        public HitContext(IAttacker attacker, RaycastHit raycastHit)
        {
            Attacker = attacker;
            RaycastHit = raycastHit;
        }
        
        public IAttacker Attacker { get; }
        public RaycastHit RaycastHit { get; }
    }
}