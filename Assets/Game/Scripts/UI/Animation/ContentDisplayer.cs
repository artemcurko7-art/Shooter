using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Game.Scripts.UI.Animation
{
    public class ContentDisplayer : MonoBehaviour
    {
        private readonly List<Transform> _targets = new();

        [SerializeField] private bool _readOnlyChildren;
        [SerializeField] private Ease _ease;
        [SerializeField] private float _duration;
        [SerializeField] private float _delay;

        private Vector3 _initialScale;
        private bool _initialized;
        private bool _hasBeenEnabled;

        private void OnEnable()
        {
            _hasBeenEnabled = true;

            if (!_initialized)
                return;

            PlayAnimation();
        }

        private void OnDisable()
        {
            if (!_initialized)
                return;

            ResetTargets();
        }

        public void InitTargets(List<Transform> items)
        {
            _targets.Clear();

            if (_readOnlyChildren || items == null || items.Count == 0)
            {
                CollectChildren();
            }
            else
            {
                foreach (var item in items.Where(item => item != null))
                {
                    _targets.Add(item);
                }
            }

            if (_targets.Count == 0)
                return;

            _initialScale = _targets[0].localScale;
            _initialized = true;

            if (_hasBeenEnabled && gameObject.activeInHierarchy)
                PlayAnimation();
        }

        private void CollectChildren()
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                _targets.Add(transform.GetChild(i));
            }
        }

        private void ResetTargets()
        {
            foreach (var target in _targets.Where(target => target != null))
            {
                target.DOKill();
                target.localScale = Vector3.zero;
            }
        }

        private void PlayAnimation()
        {
            if (_targets.Count == 0)
                return;

            var delay = 0f;

            foreach (var target in _targets.Where(target => target != null))
            {
                target.DOKill();
                target.localScale = Vector3.zero;

                target
                    .DOScale(_initialScale, _duration)
                    .SetEase(_ease)
                    .SetDelay(delay);

                delay += _delay;
            }
        }
    }
}