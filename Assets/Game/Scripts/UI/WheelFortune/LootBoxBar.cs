using System;
using Game.Scripts.UI.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.WheelFortune
{
    public class LootBoxBar : MonoBehaviour
    {
        [SerializeField] private TMP_Text _boxName;
        [SerializeField] private TMP_Text _boxCount;
        [SerializeField] private TMP_Text _keyCount;
        [SerializeField] private Image _boxIcon;
        [SerializeField] private Image _keyIcon;
        [SerializeField] private TMP_Text _buttonName;
        [SerializeField] private Button _openButton;
        [SerializeField] private Image _buttonBackground;

        [SerializeField] private Color _enableColor = Color.green;
        [SerializeField] private Color _disableColor = Color.red;

        private BoxesData.Box _box;
        private Action<BoxesData.Box> _onOpen;

        private void OnEnable()
        {
            if (_openButton)
                _openButton.onClick.AddListener(Open);
        }

        private void OnDisable()
        {
            if (_openButton)
                _openButton.onClick.RemoveListener(Open);
        }

        public void Init(BoxesData.Box box, Action<BoxesData.Box> onOpen)
        {
            _box = box;
            _onOpen = onOpen;

            if (_box == null)
                return;

            if (_boxName)
                _boxName.text = _box.box ? Localization.GetNameTranslations(_box.box.Type) : string.Empty;

            if (_boxCount)
            {
                _boxCount.text = _box.boxCount.ToString();
                _boxCount.color = _box.boxCount > 0 ? _enableColor : _disableColor;
            }

            if (_keyCount)
            {
                _keyCount.text = _box.keyCount.ToString();
                _keyCount.color = _box.keyCount > 0 ? _enableColor : _disableColor;
            }

            if (_boxIcon)
                _boxIcon.sprite = _box.box ? _box.box.GetIcon(_box.boxCount) : null;

            if (_keyIcon)
                _keyIcon.sprite = _box.key ? _box.key.GetIcon(_box.keyCount) : null;

            SwitchButton();
        }

        private void Open()
        {
            if (_box is not { boxCount: > 0 } || _box.keyCount <= 0)
                return;

            _onOpen?.Invoke(_box);
        }

        private void SwitchButton()
        {
            if (_box == null)
                return;

            var isAvailable = _box.boxCount > 0 && _box.keyCount > 0;

            if (_buttonBackground)
                _buttonBackground.color = isAvailable ? _enableColor : _disableColor;

            if (_openButton)
                _openButton.enabled = isAvailable;

            if (_buttonName)
            {
                _buttonName.text = isAvailable
                    ? Localization.GetAvailableButtonText()
                    : Localization.GetNotAvailableButtonText();
            }
        }
    }
}