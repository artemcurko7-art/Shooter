using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Game.Scripts.UI.DailyGift
{
    public class SliderGifts : MonoBehaviour
    {
        private readonly int _maxGiftsCount = 3;

        [SerializeField] private Slider _slider;
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
            }
        }

        [Serializable]
        public class GiftDay
        {
            public TMP_Text day;
            public int dayCount;
            public Image dayBackground;
            public Image giftIcon;

            public void Init()
            {
                day.text = dayCount.ToString();
            }
        }
    }
}