using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Scripts.PoolMono;
using UnityEngine;

namespace Game.Scripts.HitImpacted
{
    public class FlashHitImpacted : HitImpactedObserver
    {
        private const int Milliseconds = 1000;
        private readonly Material _flash;
        private readonly float _cooldownFlash;
        private CancellationTokenSource _cancellationTokenSource;
        
        public FlashHitImpacted(Material flash, float cooldownFlash, ProjectilePool projectilePool) : base(projectilePool)
        {
            _flash = flash;
            _cooldownFlash = cooldownFlash;
        }

        protected override void OnHitImpacted(RaycastHit hit)
        {
            var renderer = hit.transform.GetComponentInChildren<Renderer>();

            _cancellationTokenSource = new CancellationTokenSource();
            
            if (renderer != null)
                StartAsync(_cancellationTokenSource.Token, renderer).Forget();
        }

        private async UniTaskVoid StartAsync(CancellationToken token, Renderer renderer)
        {
            if (token.IsCancellationRequested == false)
            {  
                Material current = renderer.material;
                renderer.material = _flash;

                float calculationTime = Milliseconds * _cooldownFlash;

                await UniTask.Delay((int)calculationTime, cancellationToken: token);
                
                renderer.material = current;
            }
            
            _cancellationTokenSource.Cancel();
        }
    }
}