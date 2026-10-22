using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Game.Scripts.UI.Animation
{
    public class ContentDisplayer : UIAnimation
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
                return;
            }

            if (items == null)
                return;

            foreach (var item in items)
            {
                if (item)
                    AddTarget(item);
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

                if (child)
                    AddTarget(child);
            }
        }

        private void AddTarget(Transform target)
        {
            if (!target)
                return;

            if (!_targets.Contains(target))
                _targets.Add(target);

            if (!_initialScales.ContainsKey(target))
                _initialScales.Add(target, target.localScale);
        }

        private void ResetTargets()
        {
            foreach (var target in _targets.Where(target => target))
            {
                target.DOKill();

                if (_initialScales.TryGetValue(target, out var initialScale))
                    target.localScale = initialScale;
            }
        }

        private void PlayAnimation()
        {
            if (_targets.Count == 0)
                return;

            var delay = 0f;

            foreach (var target in _targets)
            {
                if (!target || !_initialScales.TryGetValue(target, out var initialScale))
                    continue;

                target.DOKill();
                target.localScale = Vector3.zero;

                target.DOScale(initialScale, _duration)
                    .SetEase(_ease)
                    .SetDelay(delay);

                delay += _delay;
            }
        }
    }
}