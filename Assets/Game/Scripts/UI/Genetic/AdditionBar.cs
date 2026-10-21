using Game.Scripts.UI.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Genetic
{
    [RequireComponent(typeof(RectTransform))]
    public class AdditionBar : MonoBehaviour, IGeneticElement
    {
        [Header("Компоненты")]
        [SerializeField] private Button _button;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _count;

        private AdditionSystem _additionSystem;
        private RectTransform _rectTransform;

        public RewardDefinition Definition { get; private set; }
        public int Count { get; private set; }

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();

            if (_button)
                _button.onClick.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            if (_button)
                _button.onClick.RemoveListener(OnClick);
        }

        public void Init(AdditionSystem additionSystem, RewardDefinition definition, int count)
        {
            _additionSystem = additionSystem;
            Definition = definition;
            Count = Mathf.Max(1, count);

            Refresh();
        }

        public void Refresh()
        {
            if (!Definition)
                return;

            if (_icon)
                _icon.sprite = Definition.GetIcon(Count);

            if (_count)
            {
                _count.text = Count > 1 ? $"x{Count}" : string.Empty;
                _count.gameObject.SetActive(Count > 1);
            }
        }

        public void UpdateDisplay()
        {
            Refresh();
        }

        public void OnClick()
        {
            if (!_additionSystem || !Definition)
                return;

            if (!_rectTransform)
                _rectTransform = GetComponent<RectTransform>();

            _additionSystem.OpenPreview(Definition, Count, _rectTransform.position);
        }
    }
}