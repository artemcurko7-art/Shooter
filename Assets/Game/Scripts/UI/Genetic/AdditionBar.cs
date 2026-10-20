using Game.Scripts.UI.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Genetic
{
    [RequireComponent(typeof(RectTransform))]
    public class AdditionBar : MonoBehaviour, IGeneticElement
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _count;

        public RewardDefinition Definition { get; private set; }
        public int Count { get; private set; }

        private void Awake()
        {
            Refresh();
        }

        public void Init(RewardDefinition definition, int count)
        {
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

            if (!_count)
                return;

            _count.text = Count > 1 ? $"x{Count}" : string.Empty;
            _count.gameObject.SetActive(Count > 1);
        }
    }
}