using System;
using UnityEngine;

namespace Game.Scripts.UI.Animation
{
    public class IconScalerGroup : UIAnimation
    {
        [Header("Авто сбор из дочерних объектов")]
        [SerializeField] private bool _autoCollectChildren = false;

        [Header("Список иконок (заполняется вручную, если авто сбор выключен)")]
        [SerializeField] private IconScaler[] _icons = Array.Empty<IconScaler>();

        [SerializeField] private int _startSelectedIndex = 0;

        private IconScaler _selectedIcon;

        public event Action<string> NameChanged;
        public event Action<Vector3> IconSelected;

        private void Awake()
        {
            if (_autoCollectChildren)
            {
                CollectChildren();
            }
        }

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

        private void CollectChildren()
        {
            _icons = GetComponentsInChildren<IconScaler>(true);

            Array.Sort(_icons, (a, b) =>
                string.Compare(a.transform.GetSiblingIndex().ToString(), 
                               b.transform.GetSiblingIndex().ToString(), StringComparison.Ordinal));

            if (_icons.Length == 0)
            {
                Debug.LogWarning($"[IconScalerGroup] Авто сбор включен, но дочерних IconScaler не найдено на {gameObject.name}.");
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
