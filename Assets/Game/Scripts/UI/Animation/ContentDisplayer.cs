using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Game.Scripts.UI.Animation
{
    public class ContentDisplayer : MonoBehaviour
    {
        private readonly List<Transform> _children = new();

        [SerializeField] private Ease _ease;
        [SerializeField] private float _duration;
        [SerializeField] private float _delay;

        private bool _childrenReseted;
        private Vector3 _initialScale;

        private void OnEnable()
        {
            AnimateChildren();
        }

        private void ResetChildren()
        {
            foreach (var child in _children)
            {
                child.localScale = Vector3.zero;
            }

            _childrenReseted = true;
        }

        public void InitChildren()
        {
            _children.Clear();

            for (var i = 0; i < transform.childCount; i++)
            {
                _children.Add(transform.GetChild(i));
            }

            _initialScale = _children.FirstOrDefault()!.transform.localScale;
        }

        private void AnimateChildren()
        {
            if (!_childrenReseted) ResetChildren();

            float delay = 0;

            foreach (var child in _children)
            {
                child
                    .DOScale(_initialScale, _duration)
                    .SetEase(_ease)
                    .SetDelay(delay);

                delay += _delay;
            }

            _childrenReseted = false;
        }
    }
}