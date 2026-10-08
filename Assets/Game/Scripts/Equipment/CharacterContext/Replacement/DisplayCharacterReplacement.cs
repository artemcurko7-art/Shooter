using System;
using Game.Scripts.Equipment.CharacterContext.Handler;
using Game.Scripts.Equipment.CharacterContext.Repository;
using Game.Scripts.Equipment.CharacterContext.Type;
using Game.Scripts.Equipment.Repository;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.SquadContext.Type;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.Equipment.CharacterContext.Replacement
{
    public class DisplayCharacterReplacement : MonoBehaviour
    {
        [Header("Dropped")]
        // [SerializeField] private Image _rarityDropped; // Подумать что можно с этим сделать
        // [SerializeField] private TMP_Text _nameDropped;
        // [SerializeField] private Image _iconDropped;
        [SerializeField] private TMP_Text _levelDropped;
        [SerializeField] private TMP_Text _healthDropped;
        [SerializeField] private TMP_Text _attackDropped;
        [SerializeField] private TMP_Text _defenceDropped;
        [SerializeField] private TMP_Text _criticalChanceDropped;
        [SerializeField] private TMP_Text _criticalDamageDropped;
        
        [Header("Dragged")]
        //[SerializeField] private Image _rarityDragged; // Подумать что можно с этим сделать
        [SerializeField] private TMP_Text _nameDragged;
        [SerializeField] private Image _iconDragged;
        [SerializeField] private TMP_Text _levelDragged;
        [SerializeField] private TMP_Text _healthDragged;
        [SerializeField] private TMP_Text _attackDragged;
        [SerializeField] private TMP_Text _defenceDragged;
        [SerializeField] private TMP_Text _criticalChanceDragged;
        [SerializeField] private TMP_Text _criticalDamageDragged;

        private FreeSlotRegistry<SquadNumberType, CharacterSlot> _freeSlotRegistry;
        private CharacterSlotHandler _handler;
        
        public RectTransform RectTransform { get; private set; }
        
        [Inject]
        public void Construct(FreeSlotRegistry<SquadNumberType, CharacterSlot> freeSlotRegistry, CharacterSlotHandler handler)
        {
            _freeSlotRegistry = freeSlotRegistry;
            _handler = handler;
            
            RectTransform = GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            //_rarityDropped.sprite = _freeSlotRegistry.EquippedSlots[CharacterPlaceDropSlotType.First].Rarity.sprite;
            // _nameDropped.text = _freeSlotRegistry.EquippedSlots[CharacterPlaceDropSlotType.First].Name;
            // _iconDropped.sprite = _freeSlotRegistry.EquippedSlots[CharacterPlaceDropSlotType.First].Icon.sprite;
            
            _levelDropped.text = 15.ToString();
            _healthDropped.text = _freeSlotRegistry.EquippedSlots[_handler.SquadNumberType].Stats[StatType.Health].ToString();
            _attackDropped.text = _freeSlotRegistry.EquippedSlots[_handler.SquadNumberType].Stats[StatType.Damage].ToString();
            _defenceDropped.text = _freeSlotRegistry.EquippedSlots[_handler.SquadNumberType].Stats[StatType.Defence].ToString();
            _criticalChanceDropped.text = _freeSlotRegistry.EquippedSlots[_handler.SquadNumberType].Stats[StatType.CriticalChance].ToString();
            _criticalDamageDropped.text = _freeSlotRegistry.EquippedSlots[_handler.SquadNumberType].Stats[StatType.CriticalDamage].ToString();

            //_rarityDragged.sprite = _handler.DraggedSlot.Rarity.sprite;
            _nameDragged.text = _handler.DraggedSlot.Name;
            _iconDragged.sprite = _handler.DraggedSlot.Icon.sprite;
            _levelDragged.text = 3.ToString();
            _healthDragged.text = _handler.DraggedSlot.Stats[StatType.Health].ToString();
            _attackDragged.text = _handler.DraggedSlot.Stats[StatType.Damage].ToString();
            _defenceDragged.text = _handler.DraggedSlot.Stats[StatType.Defence].ToString();
            _criticalChanceDragged.text = _handler.DraggedSlot.Stats[StatType.CriticalChance].ToString();
            _criticalDamageDragged.text = _handler.DraggedSlot.Stats[StatType.CriticalDamage].ToString();
        }
    }
}