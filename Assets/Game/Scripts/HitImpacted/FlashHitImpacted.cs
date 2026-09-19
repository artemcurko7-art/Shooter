using DG.Tweening;
using Game.Scripts.PoolMono;
using UnityEngine;

namespace Game.Scripts.HitImpacted
{
    public class FlashHitImpacted : HitImpactedObserver
    {
        private readonly Material _flash;
        private Tween _tween;
        private Material _currentMaterial;
        
        public FlashHitImpacted(Material flash, BulletPool bulletPool) : base(bulletPool)
        {
            _flash = flash;
        }

        protected override void OnHitImpacted(RaycastHit hit)
        {
            var renderer = hit.collider.GetComponentInChildren<Renderer>();

            _tween?.Kill();
            
            _tween = renderer.material
                .DOColor(_flash.color, 0.08f)
                .SetLoops(2, LoopType.Yoyo);
        }
    }
}