using Game.Scripts.UI.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Genetic
{
    [RequireComponent(typeof(RectTransform))]
    public class AdditionBar : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _count;

        private RectTransform _rectTransform;

        public RewardDefinition Definition { get; set; }
        public int Count { get; set; }

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void Init(RewardDefinition definition, int count)
        {
            Definition = definition;
            Count = Mathf.Max(1, count);

            if (!_rectTransform)
                _rectTransform = GetComponent<RectTransform>();

            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            if (!Definition)
            {
                Debug.LogWarning("[AdditionBar] Не назначена награда!", this);
                return;
            }

            if (_icon)
                _icon.sprite = Definition.GetIcon(Count);

            if (!_count) return;
            _count.text = Count > 1 ? $"x{Count}" : string.Empty;
            _count.gameObject.SetActive(Count > 1);
        }
    }
}