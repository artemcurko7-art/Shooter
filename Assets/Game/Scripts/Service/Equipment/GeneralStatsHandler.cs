using System;
using System.Collections.Generic;
using Game.Scripts.CharacterContext.Data;
using Game.Scripts.Equipment;
using Game.Scripts.Equipment.DragInDrop;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.Service.Subscriber;

namespace Game.Scripts.Service.Equipment
{
    public class GeneralStatsHandler : ISubscriber
    {
        private readonly CharacterData _characterData;
        private readonly DropSlot[] _dropSlots;
        private readonly Dictionary<StatType, int> _stats = new();
        
        public GeneralStatsHandler(CharacterData characterData, DropSlot[] dropSlots)
        {
            _characterData = characterData;
            _dropSlots = dropSlots;
            
            Fill();
        }
        
        public IReadOnlyDictionary<StatType, int> Stats => _stats;
        
        public void Subscribe()
        {
            foreach (var dropSlot in _dropSlots)
            {
                dropSlot.Dropped += OnDropped;
                dropSlot.Removed += OnRemoved;
            }
        }

        public void Unsubscribe()
        {
            foreach (var dropSlot in _dropSlots)
            {
                dropSlot.Dropped -= OnDropped;
                dropSlot.Removed -= OnRemoved;
            }
        }

        private void OnDropped(Slot slot)
        {
            _stats[slot.EquipmentItem.MainStat.Type] += slot.EquipmentItem.MainStat.Value;
            
            foreach (var stat in slot.EquipmentItem.AdditionalStats)
                _stats[stat.Type] += stat.Value;
        }

        private void OnRemoved(Slot slot)
        {
            _stats[slot.EquipmentItem.MainStat.Type] -= slot.EquipmentItem.MainStat.Value;
            
            foreach (var stat in slot.EquipmentItem.AdditionalStats)
                _stats[stat.Type] -= stat.Value;
        }
        
        private void Fill()
        {
            foreach (var type in Enum.GetValues(typeof(StatType)))
            {
                if ((StatType)type == StatType.None)
                    continue;
                
                _stats.Add((StatType)type, 0);
            }

            foreach (var character in _characterData.Characters) // test
            {
                foreach (var stat in character.Value.Stats)
                    _stats[stat.Type] += stat.Value;
            }
        }
    }
}