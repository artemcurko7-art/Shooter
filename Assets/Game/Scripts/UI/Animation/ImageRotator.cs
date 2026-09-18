using DG.Tweening;
using UnityEngine;

namespace Game.Scripts.UI.Animation
{
    public class ImageRotator : MonoBehaviour
    {
        [SerializeField] private float _duration = 2f;
        [SerializeField] private Ease _ease = Ease.Linear;

        private Tween _rotateTween;

        private void Start()
        {
            StartRotation();
        }

        private void OnDestroy()
        {
            _rotateTween?.Kill();
        }

        private void StartRotation()
        {
            _rotateTween = transform
                .DORotate(new Vector3(0f, 0f, 360f), _duration, RotateMode.FastBeyond360)
                .SetEase(_ease)
                .SetLoops(-1, LoopType.Restart);
        }
    }
}