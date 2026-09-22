using Game.Scripts.UI.DailyReward;
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
        private InAppData.InAppGroup.InApp _inApp;

        public void Init(InAppData.InAppGroup.InApp inApp, bool isAvailable, bool isTaken)
        {
            _inApp = inApp;

            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            if (_inApp == null) return;

            _frame.sprite = _inApp.frame;
            _icon.sprite = _inApp.icon;
            _count.text = _inApp.count.ToString();

            _checkMark.SetActive(_isTaken);
            _darkFrame.SetActive(!_isAvailable);
        }
    }
}