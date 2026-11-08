using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.UI.Animation
{
    public enum IdleAnimationType
    {
        Breathe,
        Float,
        Wobble,
        Punch,
        Jelly,
        Bounce,
        CoinSpin,
        Shake
    }

    public class IdleScaler : UIAnimation
    {
        [Header("Тип анимации")]
        [SerializeField] private IdleAnimationType _type = IdleAnimationType.Breathe;

        [Header("Общие параметры")]
        [SerializeField] private float _scale = 1.05f;
        [SerializeField] private float _duration = 0.8f;
        [SerializeField] private float _angle = 3f;
        [SerializeField, Min(0f)] private float _floatOffset = 6f;

        private Vector3 _initialScale;
        private Vector3 _initialPosition;
        private Quaternion _initialRotation;

        private void Awake()
        {
            _initialScale = transform.localScale;
            _initialPosition = transform.localPosition;
            _initialRotation = transform.localRotation;
        }

        private void OnEnable()
        {
            ResetTransform();
            PlayIdleAnimation();
        }

        private void OnDisable()
        {
            transform.DOKill();
            ResetTransform();
        }

        private void ResetTransform()
        {
            transform.localScale = _initialScale;
            transform.localPosition = _initialPosition;
            transform.localRotation = _initialRotation;
        }

        private void PlayIdleAnimation()
        {
            transform.DOKill();

            switch (_type)
            {
                case IdleAnimationType.Breathe:
                    transform.DOScale(_initialScale * _scale, _duration)
                        .SetEase(Ease.InOutSine)
                        .SetLoops(-1, LoopType.Yoyo);

                    transform.DOLocalRotate(new Vector3(0f, 0f, _angle), _duration)
                        .SetEase(Ease.InOutSine)
                        .SetLoops(-1, LoopType.Yoyo);
                    break;

                case IdleAnimationType.Float:
                    transform.DOLocalMoveY(_initialPosition.y + _floatOffset, _duration)
                        .SetEase(Ease.InOutSine)
                        .SetLoops(-1, LoopType.Yoyo);

                    transform.DOScale(_initialScale * _scale, _duration * 2f)
                        .SetEase(Ease.InOutSine)
                        .SetLoops(-1, LoopType.Yoyo);
                    break;

                case IdleAnimationType.Wobble:
                    transform.DOLocalRotate(new Vector3(0f, 0f, _angle * 3f), _duration)
                        .SetEase(Ease.InOutSine)
                        .SetLoops(-1, LoopType.Yoyo);
                    break;

                case IdleAnimationType.Punch:
                    PunchLoop();
                    break;

                case IdleAnimationType.Jelly:
                    transform.DOScaleX(_initialScale.x * _scale, _duration)
                        .SetEase(Ease.InOutSine)
                        .SetLoops(-1, LoopType.Yoyo);

                    transform.DOScaleY(_initialScale.y * (2f - _scale), _duration)
                        .SetEase(Ease.InOutSine)
                        .SetLoops(-1, LoopType.Yoyo);
                    break;

                case IdleAnimationType.Bounce:
                    var jump = DOTween.Sequence().SetLoops(-1);

                    jump.Append(transform.DOLocalMoveY(_initialPosition.y + _floatOffset * 3f, 0.3f)
                        .SetEase(Ease.OutQuad));

                    jump.Append(transform.DOLocalMoveY(_initialPosition.y, 0.25f)
                        .SetEase(Ease.InQuad));

                    jump.Append(transform.DOPunchScale(new Vector3(0.04f, -0.08f, 0f), 0.15f, 2, 0.4f));

                    jump.AppendInterval(1.5f);
                    break;

                case IdleAnimationType.CoinSpin:
                    transform.DOScaleY(0.05f, _duration)
                        .SetEase(Ease.InOutSine)
                        .SetLoops(-1, LoopType.Yoyo);
                    break;

                case IdleAnimationType.Shake:
                    var shake = DOTween.Sequence().SetLoops(-1);

                    shake.Append(transform.DOLocalMoveX(_initialPosition.x + 1.5f, 0.05f)
                        .SetEase(Ease.OutQuad));

                    shake.Append(transform.DOLocalMoveX(_initialPosition.x - 1.5f, 0.05f)
                        .SetEase(Ease.InOutQuad));

                    shake.Append(transform.DOLocalMoveX(_initialPosition.x, 0.05f)
                        .SetEase(Ease.InQuad));

                    shake.AppendInterval(2f);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void PunchLoop()
        {
            var delay = Random.Range(2f, 5f);

            DOVirtual.DelayedCall(delay, () =>
            {
                if (!isActiveAndEnabled) return;

                transform.DOPunchScale(_initialScale * (_scale - 1f) * 2f, _duration, 3, 0.5f)
                    .OnComplete(PunchLoop);
            });
        }
    }
}
