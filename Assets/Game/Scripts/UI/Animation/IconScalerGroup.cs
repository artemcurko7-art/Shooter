using System;
using UnityEngine;

namespace Game.Scripts.UI.Animation
{
    public class IconScalerGroup : MonoBehaviour
    {
        [SerializeField] private IconScaler[] _icons;
        [SerializeField] private int _startSelectedIndex = 0;

        private IconScaler _selectedIcon;

        public event Action<string> NameChanged;
        public event Action<Vector3> IconSelected;

        private void Start()
        {
            if (_icons == null || _icons.Length == 0)
                return;

            for (var i = 0; i < _icons.Length; i++)
            {
                if (!_icons[i])
                    continue;

                _icons[i].SetSelected(i == _startSelectedIndex, false);
            }

            if (_startSelectedIndex >= 0 &&
                _startSelectedIndex < _icons.Length)
            {
                _selectedIcon = _icons[_startSelectedIndex];
            }
        }

        public void Select(IconScaler icon, string word)
        {
            if (!icon || icon == _selectedIcon)
                return;

            if (_selectedIcon)
                _selectedIcon.SetSelected(false);

            _selectedIcon = icon;
            _selectedIcon.SetSelected(true);

            IconSelected?.Invoke(_selectedIcon.transform.position);

            if (word == string.Empty) return;
            NameChanged?.Invoke(word);
        }
    }
}