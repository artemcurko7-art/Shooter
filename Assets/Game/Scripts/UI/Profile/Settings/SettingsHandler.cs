using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Game.Scripts.UI.Profile.Settings
{
    public class SettingsHandler : MonoBehaviour
    {
        private const int MAX_VALUE = 1;
        private const float ENABLED_THRESHOLD = 0.1f;

        private readonly Dictionary<Slider, Image> _handles = new();
        
        [SerializeField] private Slider _soundSlider;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _animotionSlider;

        private void Awake()
        {
            ConfigureSliders();
        }

        private void OnEnable()
        {
            _soundSlider?.onValueChanged.AddListener(value => OnSettingChanged(_soundSlider, value));
            _musicSlider?.onValueChanged.AddListener(value => OnSettingChanged(_musicSlider, value));
            _animotionSlider?.onValueChanged.AddListener(value => OnSettingChanged(_animotionSlider, value));
        }

        private void OnDisable()
        {
            _soundSlider?.onValueChanged.RemoveAllListeners();
            _musicSlider?.onValueChanged.RemoveAllListeners();
            _animotionSlider?.onValueChanged.RemoveAllListeners();
        }

        public void Setup()
        {
            ApplySetting(_soundSlider, YG2.saves.Sound);
            ApplySetting(_musicSlider, YG2.saves.Music);
            ApplySetting(_animotionSlider, YG2.saves.Animation);
        }

        private void ApplySetting(Slider slider, bool isEnabled)
        {
            if (!slider) return;

            slider.SetValueWithoutNotify(isEnabled ? MAX_VALUE : 0);
            UpdateHandleColor(slider, isEnabled);
        }

        private void ConfigureSliders()
        {
            ConfigureSlider(_soundSlider);
            ConfigureSlider(_musicSlider);
            ConfigureSlider(_animotionSlider);
        }

        private void ConfigureSlider(Slider slider)
        {
            if (!slider) return;

            slider.maxValue = MAX_VALUE;
            slider.wholeNumbers = true;
            
            if (!slider.handleRect || !slider.handleRect.TryGetComponent(out Image image)) return;
            
            _handles[slider] = image;
            image.color = StatusColors.GetColor(slider.value >= ENABLED_THRESHOLD);
        }

        private void OnSettingChanged(Slider slider, float value)
        {
            if (!slider) return;

            var isEnabled = value >= ENABLED_THRESHOLD;

            if (slider == _soundSlider)
                YG2.saves.Sound = isEnabled;
            else if (slider == _musicSlider)
                YG2.saves.Music = isEnabled;
            else if (slider == _animotionSlider)
                YG2.saves.Animation = isEnabled;

            YG2.SaveProgress();

            UpdateHandleColor(slider, isEnabled);
        }

        private void UpdateHandleColor(Slider slider, bool isEnabled)
        {
            if (_handles.TryGetValue(slider, out var handle))
                handle.color = StatusColors.GetColor(isEnabled);
        }
    }
}