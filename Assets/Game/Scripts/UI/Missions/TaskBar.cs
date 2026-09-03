using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Game.Scripts.UI.Missions
{
    public class TaskBar : MonoBehaviour
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _content;
        [SerializeField] private TMP_Text _reward;
        [SerializeField] private Image _rewardIcon;
        [SerializeField] private Image _paperIcon;
        [SerializeField] private Image _succesIcon;
        [SerializeField] private RectTransform _starsParent;
        [SerializeField] private Button _button;
        [SerializeField] private Image _starPrefab;
        [SerializeField] private GameObject _rewardBackground;
        [SerializeField] private GameObject _progressBackground;

        [Header("Визуал")]
        [SerializeField] private Color _easyColor;
        [SerializeField] private Color _normalColor;
        [SerializeField] private Color _hardColor;

        [Header("Настройки анимации")]
        [SerializeField] private Vector2 _scalerSize;
        [SerializeField] private float _duration = 0.3f;
        [SerializeField] private float _sleepDelay = 3f;
        [SerializeField] private Ease _ease = Ease.OutBack;

        private LayoutGroup _parentLayout;
        private TasksData.Task _task;
        private RectTransform _rect;
        private Tween _sizeTween;
        private Tween _posTween;
        private Tween _sleepTween;
        private bool _isScaled;
        private Vector2 _originalSize;
        private Vector2 _originalPos;
        private bool _isDone = false;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
            _sleepTween?.Kill();

            FastCloseScale();
        }

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();

            if (!_parentLayout)
                _parentLayout = GetComponentInParent<LayoutGroup>();
        }

        private void Start()
        {
            _originalSize = _rect.sizeDelta;
            _originalPos = _rect.anchoredPosition;
            
            SwitchBackgrounds(false);

            UpdateDisplay();
        }

        private void SwitchBackgrounds(bool isActive)
        {
            _rewardBackground.SetActive(isActive);
            _progressBackground.SetActive(isActive);
        }

        public void Init(TasksData.Task task)
        {
            _task = task;
            InitializeDifficulty();
        }

        private void OnButtonClick()
        {
            ScaleAlt();
        }

        private void InitializeDifficulty()
        {
            var count = _task.difficulty;

            var color = count switch
            {
                1 => _easyColor,
                2 => _normalColor,
                3 => _hardColor,
                _ => Color.white
            };

            for (var i = 0; i < count; i++)
            {
                var star = Instantiate(_starPrefab, _starsParent);
                star.color = color;
            }
        }

        private void UpdateDisplay()
        {
            if (_task == null) return;

            _paperIcon.gameObject.SetActive(!_isDone);
            _succesIcon.gameObject.SetActive(_isDone);

            _content.text = _task.GetLocalizedTask(YG2.lang);
        }

        private void FastCloseScale()
        {
            SwitchBackgrounds(false);

            _sizeTween?.Kill();
            _posTween?.Kill();
            _sleepTween?.Kill();
            _isScaled = false;

            _rect.sizeDelta = _originalSize;
            _rect.anchoredPosition = _originalPos;

            if (_parentLayout)
                LayoutRebuilder.MarkLayoutForRebuild(_parentLayout.GetComponent<RectTransform>());
        }


        private void ScaleAlt()
        {
            _sizeTween?.Kill();
            _posTween?.Kill();
            _sleepTween?.Kill();

            SwitchBackgrounds(true);

            _isScaled = !_isScaled;

            var targetSize = _isScaled ? _scalerSize : _originalSize;
            var currentSize = _rect.sizeDelta;
            var currentPos = _rect.anchoredPosition;

            var delta = targetSize - currentSize;
            var newPos = currentPos + new Vector2(-delta.x * 0.5f, delta.y * 0.5f);

            _sizeTween = _rect
                .DOSizeDelta(targetSize, _duration)
                .SetEase(_ease)
                .OnUpdate(() =>
                {
                    if (_parentLayout)
                        LayoutRebuilder.MarkLayoutForRebuild(
                            _parentLayout.GetComponent<RectTransform>());
                });

            _posTween = _rect
                .DOAnchorPos(newPos, _duration)
                .SetEase(_ease);

            if (_isScaled)
            {
                _sleepTween = DOVirtual.DelayedCall(_sleepDelay, () =>
                {
                    _sizeTween?.Kill();
                    _posTween?.Kill();
                    _isScaled = false;

                    var curSize = _rect.sizeDelta;
                    var curPos = _rect.anchoredPosition;
                    var backDelta = _originalSize - curSize;
                    var backPos = curPos + new Vector2(-backDelta.x * 0.5f, backDelta.y * 0.5f);

                    _sizeTween = _rect
                        .DOSizeDelta(_originalSize, _duration)
                        .SetEase(_ease)
                        .OnUpdate(() =>
                        {
                            if (_parentLayout)
                                LayoutRebuilder.MarkLayoutForRebuild(
                                    _parentLayout.GetComponent<RectTransform>());
                        });

                    _posTween = _rect
                        .DOAnchorPos(backPos, _duration)
                        .SetEase(_ease);
                });
            }
        }
    }
}