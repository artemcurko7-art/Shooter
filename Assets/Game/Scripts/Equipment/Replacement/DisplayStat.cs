using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Equipment.Replacement
{
    public class DisplayStat : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _valueText;

        public void Initialize(Sprite icon, string name, int value, bool IsPercentageValue)
        {
            _icon.sprite = icon;
            _nameText.text = name;
            _valueText.text = IsPercentageValue ? $"{value}%" : value.ToString();
        }

        public void OnDisabled()
        {
            gameObject.SetActive(false); // исправить
        }
    }
}