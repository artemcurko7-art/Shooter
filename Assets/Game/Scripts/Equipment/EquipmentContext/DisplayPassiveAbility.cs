using Game.Scripts.Equipment.CharacterContext.Type;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.PassiveAbilityContext.Type;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.Equipment.EquipmentContext
{
    public class DisplayPassiveAbility : MonoInstaller
    {
        [Header("Character")]
        [SerializeField] private TMP_Text _characterNameText;
        
        [Header("Passive Ability")]
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _nameText;
        
        [Header("Specifications")]
        [SerializeField] private Image _iconType;
        [SerializeField] private TMP_Text _typeText;
        [SerializeField] private TMP_Text _targetingTypeText;
        [SerializeField] private TMP_Text _statTypeText;
        [SerializeField] private Image _iconStatType;
        [SerializeField] private TMP_Text _valueText;
        [SerializeField] private TMP_Text _cooldownText;
        [SerializeField] private TMP_Text _descriptionText;
        
        public CharacterType CharacterType { get; private set; }
        
        public void Initialize(
            CharacterType characterType,
            string characterName,
            Sprite icon,
            string name,
            Sprite iconType,
            PassiveAbilityType type,
            PassiveAbilityTargetingType targetingType,
            StatType statType,
            Sprite iconStatType,
            int value,
            float cooldown,
            string description)
        {
            CharacterType = characterType;
            _characterNameText.text = characterName;
            _icon.sprite = icon;
            _nameText.text = name;
            _iconType.sprite = iconType;
            _typeText.text = type.ToString();
            _targetingTypeText.text = targetingType.ToString();
            _statTypeText.text = statType.ToString();
            _iconStatType.sprite = iconStatType;
            _valueText.text = $"+{value}";
            _cooldownText.text = $"{cooldown} сек";
            _descriptionText.text = description;
        }
    }
}