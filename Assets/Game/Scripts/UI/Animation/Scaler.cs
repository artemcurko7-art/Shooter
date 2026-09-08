using DG.Tweening;
using UnityEngine;

namespace Game.Scripts.UI.Animation
{
    public class Scaler : MonoBehaviour
    {
        [SerializeField] private float _scale = 1.05f;
        [SerializeField] private float _duration = 0.8f;
        [SerializeField] private float _rotation = 2f;

        private Vector3 _initialScale;
        private Quaternion _initialRotation;

        private void Awake()
        {
            _initialScale = transform.localScale;
            _initialRotation = transform.localRotation;
        }

        private void OnEnable()
        {
            transform.DOKill();

            transform.localScale = _initialScale;
            transform.localRotation = _initialRotation;

            PlayIdleAnimation();
        }

        private void OnDisable()
        {
            transform.DOKill();

            transform.localScale = _initialScale;
            transform.localRotation = _initialRotation;
        }

        private void PlayIdleAnimation()
        {
            var angle = Random.Range(0f, 3f);
            angle *= Random.value < 0.5f ? -1f : 1f;

            var sequence = DOTween.Sequence();

            sequence.Append(
                transform.DOScale(_initialScale * _scale, _duration)
                    .SetEase(Ease.InOutSine)
            );

            sequence.Join(
                transform.DOLocalRotate(
                    new Vector3(0f, 0f, angle),
                    _duration
                ).SetEase(Ease.InOutSine)
            );

            sequence.Append(
                transform.DOScale(_initialScale, _duration)
                    .SetEase(Ease.InOutSine)
            );

            sequence.Join(
                transform.DOLocalRotate(
                    _initialRotation.eulerAngles,
                    _duration
                ).SetEase(Ease.InOutSine)
            );

            sequence.OnComplete(PlayIdleAnimation);
        }
    }
}