using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Shop
{
    public class CategoryButton : MonoBehaviour
    {
        private readonly Color _activeColor = Color.white;

        [SerializeField] private TMP_Text _name;
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _underline;
        [SerializeField] private Color _disableColor;
        [SerializeField] private Categories _categories;
        [SerializeField] private RectTransform _target;

        private bool _isActive;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            _categories.GetTarget();
        }

        public void SetActive(bool isActive)
        {
            _isActive = isActive;
        }

        public void UpdateVisual()
        {
            _underline.SetActive(_isActive);
            _name.color = _isActive ? _activeColor : _disableColor;
        }
    }
}