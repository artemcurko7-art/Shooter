using System;
using System.Collections.Generic;
using Game.Scripts.UI.Animation;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using YG;
using Image = UnityEngine.UI.Image;
using Slider = UnityEngine.UI.Slider;

namespace Game.Scripts.UI.DailyGift
{
    public class SliderGifts : MonoBehaviour
    {
        private const int MAX_GIFTS_COUNT = 3;

        [SerializeField] private Slider _slider;
        [SerializeField] private SliderAnimotor _sliderAnimotor;
        [SerializeField] private Color _unlockedColor = Color.white.WithAlpha(100);
        [SerializeField] private Color _defaultColor = Color.blue.WithAlpha(100);
        [SerializeField] private int _maxDaysCount = 21;
        [SerializeField] private List<GiftDay> _giftDays;

        private void Awake()
        {
            InitializeSlider();
            InitializeGifts();
        }

        private void Start()
        {
            UpdateVisual();
        }

        private void InitializeSlider()
        {
            _slider.minValue = 0f;
            _slider.maxValue = _maxDaysCount;

            _sliderAnimotor.InitTarget(YG2.saves.TotalCollectedGifts);
        }

        private void InitializeGifts()
        {
            for (var i = 0; i < _giftDays.Count && i < MAX_GIFTS_COUNT; i++)
            {
                _giftDays[i].Init();
            }
        }

        public void Play()
        {
            _sliderAnimotor.Play();
        }

        public void UpdateVisual()
        {
            foreach (var giftDay in _giftDays)
            {
                giftDay.UpdateVisual(_defaultColor, _unlockedColor);
            }
        }

        [Serializable]
        public class GiftDay
        {
            public int id;
            public TMP_Text day;
            public int dayCount;
            public Image dayBackground;
            public GiftBox giftBox;

            public void Init()
            {
                if (day != null)
                    day.text = dayCount.ToString();

                if (giftBox != null)
                    giftBox.Init(this);
            }

            public void UpdateVisual(Color defaultColor, Color unlockedColor)
            {
                var collectedGifts = YG2.saves.TotalCollectedGifts;
                var isUnlocked = collectedGifts >= dayCount;
                var isCollected = YG2.saves.TakenDailyGiftBoxes.Contains(id);

                if (dayBackground != null)
                    dayBackground.color = isUnlocked ? unlockedColor : defaultColor;

                if (giftBox != null)
                    giftBox.UpdateVisible(isUnlocked, isCollected);
            }

            public void Collect()
            {
                var collectedGifts = YG2.saves.TotalCollectedGifts;

                if (collectedGifts < dayCount)
                    return;

                if (YG2.saves.TakenDailyGiftBoxes.Contains(id))
                    return;

                YG2.saves.TakenDailyGiftBoxes.Add(id);
                YG2.SaveProgress();
            }
        }
    }
}