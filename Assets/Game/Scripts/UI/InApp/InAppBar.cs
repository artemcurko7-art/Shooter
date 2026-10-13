using Game.Scripts.UI.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.InApp
{
    public class InAppBar : MonoBehaviour
    {
        [SerializeField] private Image _frame;
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _darkFrame;
        [SerializeField] private GameObject _checkMark;
        [SerializeField] private TMP_Text _count;

        private bool _isAvailable;
        private bool _isTaken;
        private InAppData.InApp _inApp;

        public void Init(InAppData.InApp inApp, bool isAvailable, bool isTaken)
        {
            _inApp = inApp;
            _isAvailable = isAvailable;
            _isTaken = isTaken;

            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            if (_inApp == null)
                return;

            if (_frame)
                _frame.sprite = _inApp.Frame;

            var reward = _inApp.Rewards != null && _inApp.Rewards.Count > 0
                ? _inApp.Rewards[0]
                : null;

            if (reward != null)
            {
                if (_icon)
                    _icon.sprite = reward.Icon;

                if (_count)
                    _count.text = reward.Count.ToString();
            }
            else
            {
                if (_icon)
                    _icon.sprite = null;

                if (_count)
                    _count.text = string.Empty;
            }

            if (_checkMark)
                _checkMark.SetActive(_isTaken);

            if (_darkFrame)
                _darkFrame.SetActive(!_isAvailable);
        }
    }
}