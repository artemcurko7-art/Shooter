using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Game.Scripts.UI.Animation;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Challenges
{
    public class Achievements : Window
    {
        private readonly List<AchieveBar> _bars = new();

        [SerializeField] private AchievementData _data;
        [SerializeField] private AchieveBar _barPrefab;
        [SerializeField] private TMP_Text _titleAchievesCount;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private LayoutGroup _content;
        [SerializeField] private SmoothScroll _smoothScroll;

        [Header("Скролл")]
        [SerializeField] private float _scrollDuration = 0.5f;
        [SerializeField] private float _closeScrollDuration = 0.3f;

        private Vector2 _savedContentPosition;
        private bool _contentPositionSaved;
        private float _topOffset;

        private void Awake()
        {
            _topOffset = _content.padding.top;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _smoothScroll?.Stop();

            if (_scrollRect)
                _scrollRect.vertical = true;

            SetBarsInteractable(true);
        }

        private void Start()
        {
            InitializeAchieves();
        }

        private static float GetBarTopLocalY(RectTransform bar)
        {
            return bar.anchoredPosition.y + bar.rect.height * (1f - bar.pivot.y);
        }

        private void InitializeAchieves()
        {
            if (_titleAchievesCount)
                _titleAchievesCount.text = _data.Achieves.Count.ToString();

            _bars.Clear();

            foreach (var achieve in _data.Achieves)
            {
                var bar = Instantiate(
                    _barPrefab,
                    _content.transform as RectTransform
                );

                bar.Init(
                    achieve,
                    _scrollRect,
                    ScrollToBar,
                    () => OnExpandComplete(bar),
                    OnCloseComplete
                );

                _bars.Add(bar);
            }

            RebuildLayout();
        }

        private void RebuildLayout()
        {
            Canvas.ForceUpdateCanvases();

            if (_content)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(
                    _content.transform as RectTransform
                );
            }

            Canvas.ForceUpdateCanvases();
        }

        private void ScrollToBar(RectTransform targetBar)
        {
            if (!targetBar)
                return;

            var bar = targetBar.GetComponent<AchieveBar>();

            if (!bar || bar.IsDescriptionExpanded)
                return;

            if (!_scrollRect || !_scrollRect.content)
                return;

            _savedContentPosition = _scrollRect.content.anchoredPosition;
            _contentPositionSaved = true;

            SetBarsInteractable(false);

            _smoothScroll?.Stop();

            _scrollRect.StopMovement();
            _scrollRect.vertical = false;

            bar.Expand();
        }

        private void OnExpandComplete(AchieveBar bar)
        {
            if (!bar || !bar.RectTransform)
                return;

            RebuildLayout();
            ScrollAfterExpand(bar.RectTransform);
        }

        private void ScrollAfterExpand(RectTransform targetBar)
        {
            if (!_scrollRect || !_scrollRect.content || !_scrollRect.viewport)
                return;

            if (!targetBar)
                return;

            var content = _scrollRect.content;
            var targetTopLocalY = GetBarTopLocalY(targetBar);
            var targetContentY = -targetTopLocalY;

            targetContentY -= _topOffset;

            var contentHeight = content.rect.height;
            var viewportHeight = _scrollRect.viewport.rect.height;
            var maxScroll = Mathf.Max(0f, contentHeight - viewportHeight);

            targetContentY = Mathf.Clamp(
                targetContentY,
                0f,
                maxScroll
            );

            _smoothScroll?.Stop();
            _scrollRect.StopMovement();

            if (Mathf.Abs(targetContentY - content.anchoredPosition.y) < 1f)
                return;

            if (_smoothScroll)
            {
                _smoothScroll.ScrollToY(
                    targetContentY,
                    _scrollDuration
                );
            }
            else
            {
                content.anchoredPosition = new Vector2(
                    content.anchoredPosition.x,
                    targetContentY
                );
            }
        }

        private void OnCloseComplete()
        {
            if (!_scrollRect || !_scrollRect.content)
                return;

            _smoothScroll?.Stop();
            _scrollRect.StopMovement();

            if (_contentPositionSaved)
            {
                if (_smoothScroll)
                {
                    _smoothScroll.ScrollToPosition(
                        _savedContentPosition,
                        _closeScrollDuration,
                        FinishCloseScroll
                    );
                }
                else
                {
                    _scrollRect.content.anchoredPosition = _savedContentPosition;
                    FinishCloseScroll();
                }
            }
            else
            {
                FinishCloseScroll();
            }
        }

        private void FinishCloseScroll()
        {
            _contentPositionSaved = false;

            if (_scrollRect)
                _scrollRect.vertical = true;

            SetBarsInteractable(true);
        }

        private void SetBarsInteractable(bool interactable)
        {
            foreach (var bar in _bars.Where(bar => bar))
                bar.SetOpenButtonEnabled(interactable);
        }

        protected override void Show()
        {
            _transition.Open(
                _canvasGroup,
                _rectTransform,
                _openButton.transform.position,
                _scaleEase,
                _positionEase,
                _duration
            );
        }

        protected override void Hide()
        {
            if (IsTransitionActive)
                return;

            if (_transition)
                _transition.Close(
                    _canvasGroup,
                    _rectTransform
                );
        }
    }
}