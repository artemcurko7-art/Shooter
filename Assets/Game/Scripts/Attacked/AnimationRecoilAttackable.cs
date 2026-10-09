using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.WeaponContext.Shooting;
using UnityEngine;

namespace Game.Scripts.Attacked
{
    public class AnimationRecoilAttackable : AttackableObserver
    {
        public AnimationRecoilAttackable(IWeaponShooting[] shootings) : base(shootings) { }

        protected override void OnAttacked(IAttacker attacker)
        {
            //Debug.Log($"Shoot: {attacker.CharacterType}");
            
            // foreach (var hand in _handGrips)
            // {
            //     hand.DOKill(complete: true);
            //     
            //     hand.DOLocalMoveZ(-0.1f, 0.08f)
            //         .SetLoops(2, LoopType.Yoyo)
            //         .SetEase(Ease.OutQuad);
            //
            //     hand.DOPunchRotation(new Vector3(-8f, 0f, 0f), 0.15f, vibrato: 5);
            // }
        }
    }
}