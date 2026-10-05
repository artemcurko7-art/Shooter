using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Game.Scripts.UI.DailyGift
{
    public class SliderGifts : MonoBehaviour
    {
        private readonly int _maxGiftsCount = 3;

        [SerializeField] private Slider _slider;
        [SerializeField] private Color _takenColor = Color.white.WithAlpha(100);
        [SerializeField] private Color _defaultColor = Color.blue.WithAlpha(100);
        [SerializeField] private int _maxDaysCount = 21;
        [SerializeField] private List<GiftDay> _giftDays;

        private void Awake()
        {
            InitializeSlider();
            InitializeGifts();
        }

        private void InitializeSlider()
        {
            _slider.minValue = 0f;
            _slider.maxValue = _maxDaysCount;
            _slider.value = YG2.saves.TotalCollectedGifts;
        }

        private void InitializeGifts()
        {
            for (var i = 0; i < _giftDays.Count && i < _maxGiftsCount; i++)
            {
                _giftDays[i].Init();
                _giftDays[i].UpdateVisual(_defaultColor, _takenColor);
            }
        }

        [Serializable]
        public class GiftDay
        {
            public TMP_Text day;
            public int dayCount;
            public Image dayBackground;
            public Image giftIcon;
            public GiftBox giftBox;

            public void Init()
            {
                day.text = dayCount.ToString();
            }

            public void UpdateVisual(Color defaultColor, Color takenColor)
            {
                var collectedGifts = YG2.saves.TotalCollectedGifts;
                giftIcon.color = collectedGifts < dayCount ? Color.gray : Color.white;
                dayBackground.color = collectedGifts < dayCount ? defaultColor : takenColor;
                giftBox.SwitchRay(collectedGifts >= dayCount);
            }
        }
    }
}