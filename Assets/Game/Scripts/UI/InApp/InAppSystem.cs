using System.Collections.Generic;
using Game.Scripts.UI.Animation;
using UnityEngine;

namespace Game.Scripts.UI.InApp
{
    public class InAppSystem : Window
    {
        [SerializeField] private InAppGroupBar _prefab;
        [SerializeField] private InAppData _data;
        [SerializeField] private RectTransform _groupParent;

        private readonly List<InAppGroupBar> _groupBars = new();

        private void Start()
        {
            InitializeInApps();
        }

        private void InitializeInApps()
        {
            foreach (var group in _data.Groups)
            {
                var bar = Instantiate(
                    _prefab,
                    _groupParent
                );

                bar.InitializeBars(group);
                _groupBars.Add(bar);
            }
        }

        protected override void Show()
        {
            _transition.Open(
                _canvasGroup,
                _rectTransform,
                _openButton.transform.position,
                _scaleEase,
                _positionEase,
                _duration,
                FinishTransition
            );
        }

        protected override void Hide()
        {
            if (!_transition)
            {
                FinishTransition();
                return;
            }

            _transition.Close(
                _canvasGroup,
                _rectTransform,
                FinishTransition
            );
        }
    }
}