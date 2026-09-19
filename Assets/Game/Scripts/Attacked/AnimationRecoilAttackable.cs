using DG.Tweening;
using Game.Scripts.Configs;
using Game.Scripts.WeaponContext.Shooting;
using UnityEngine;

namespace Game.Scripts.Attacked
{
    public class AnimationRecoilAttackable : AttackableObserver
    {
        private readonly Transform[] _handGrips;
        
        public AnimationRecoilAttackable(Transform[] handGrips, WeaponConfig config, IWeaponShooting[] shootings) : base(config, shootings)
        {
            _handGrips = handGrips;
        }

        protected override void OnAttacked()
        {
            foreach (var hand in _handGrips)
            {
                hand.DOKill(complete: true);
                
                hand.DOLocalMoveZ(-0.1f, 0.08f)
                    .SetLoops(2, LoopType.Yoyo)
                    .SetEase(Ease.OutQuad);

                hand.DOPunchRotation(new Vector3(-8f, 0f, 0f), 0.15f, vibrato: 5);
            }
        }
    }
}