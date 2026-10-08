using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Game.Scripts.UI.Animation
{
    public class ContentDisplayer : MonoBehaviour
    {
        private readonly List<Transform> _targets = new();
        private readonly Dictionary<Transform, Vector3> _initialScales = new();

        [SerializeField] private bool _getOnlyChildren;
        [SerializeField] private bool _animateOnEnable = true;
        [SerializeField] private Ease _ease = Ease.OutBack;
        [SerializeField] private float _duration = 0.5f;
        [SerializeField] private float _delay = 0.1f;

        private void Awake()
        {
            if (_getOnlyChildren)
                CollectChildren();
        }

        private void OnEnable()
        {
            if (!_animateOnEnable)
                return;

            if (_getOnlyChildren)
                CollectChildren();

            PlayAnimation();
        }

        private void OnDisable()
        {
            ResetTargets();
        }

        public void InitTargets(List<Transform> items)
        {
            _targets.Clear();

            if (_getOnlyChildren)
            {
                CollectChildren();
            }
            else
            {
                foreach (var item in items)
                {
                    if (!item)
                        continue;

                    AddTarget(item);
                }
            }
        }

        public void Play()
        {
            if (_getOnlyChildren)
                CollectChildren();

            PlayAnimation();
        }

        private void CollectChildren()
        {
            _targets.Clear();

            for (var i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);

                if (!child)
                    continue;

                AddTarget(child);
            }
        }

        private void AddTarget(Transform target)
        {
            if (!target)
                return;

            _targets.Add(target);

            if (!_initialScales.ContainsKey(target))
                _initialScales.Add(target, target.localScale);
        }

        private void ResetTargets()
        {
            foreach (var target in _targets)
            {
                if (!target)
                    continue;

                target.DOKill();
                target.localScale = Vector3.zero;
            }
        }

        private void PlayAnimation()
        {
            if (_targets.Count == 0)
                return;

            var delay = 0f;

            foreach (var target in _targets)
            {
                if (!target)
                    continue;

                if (!_initialScales.TryGetValue(target, out var initialScale))
                    continue;

                target.DOKill();
                target.localScale = Vector3.zero;

                target
                    .DOScale(initialScale, _duration)
                    .SetEase(_ease)
                    .SetDelay(delay);

                delay += _delay;
            }
        }
    }
}