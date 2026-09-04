using Game.Scripts.MV.StatContext;
using Game.Scripts.MV.StatContext.Type;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Equipment.Replacement
{
    public class DisplayStat : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Image _upArrow;
        [SerializeField] private Image _downArrow;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _valueText;

        public void Initialize(StatType type, Sprite icon, string name, int value, bool isPercentageValue)
        {
            Type = type;
            Value = value;
            IsPercentageValue = isPercentageValue;
            _icon.sprite = icon;
            _nameText.text = name;
            _valueText.text = isPercentageValue ? $"{value}%" : value.ToString();
        }

        public StatType Type { get; private set; }
        public int Value { get; private set; }
        public bool IsPercentageValue { get; private set; }
        
        public void SetArrowDirection(bool isUp)
        {
            _upArrow.enabled = isUp;
            _downArrow.enabled = isUp == false;
        }

        public void SetArrowNeutral()
        {
            _upArrow.enabled = false;
            _downArrow.enabled = false;
        }

        public void OnDestroyed()
        {
            Destroy(gameObject);
        }
    }
}