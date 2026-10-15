using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.WheelFortune
{
    public class WheelButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _buttonImage;
        [SerializeField] private TMP_Text _buttonText;
        [SerializeField] private Sprite _greenButton;
        [SerializeField] private Sprite _redButton;

        public event Action Clicked;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        public void SetAvailable(bool isAvailable)
        {
            if (_button)
                _button.enabled = isAvailable;

            if (_buttonImage)
                _buttonImage.sprite = isAvailable ? _greenButton : _redButton;

            if (_buttonText)
            {
                _buttonText.text = isAvailable
                    ? Localization.GetSpinButtonText()
                    : Localization.GetNotAvailableButtonText();
            }
        }

        private void OnButtonClick()
        {
            Clicked?.Invoke();
        }
    }
}