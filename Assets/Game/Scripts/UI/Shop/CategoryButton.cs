using Unity.VisualScripting;

namespace Game.Scripts.UI.Shop
{
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Button))]
    public class CategoryButton : MonoBehaviour
    {
        private readonly Color _activeColor = Color.white.WithAlpha(100);

        [SerializeField] private TMP_Text _name;
        [SerializeField] private GameObject _underline;
        [SerializeField] private Color _disableColor;
        [SerializeField] private Shop _shop;
        [SerializeField] private RectTransform _target;

        private Button _button;
        private bool _isActive;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

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
            _shop.RefreshCategory(this);
            _shop.VerticalScrollMove(_target);
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