using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.WeaponContext;
using UnityEngine;

namespace Game.Scripts.HitImpacted
{
    public class HitContext
    {
        public HitContext(ShotContext shotContext, RaycastHit raycastHit)
        {
            ShotContext = shotContext;
            RaycastHit = raycastHit;
        }
        
        public ShotContext ShotContext { get; }
        public RaycastHit RaycastHit { get; }
    }
}