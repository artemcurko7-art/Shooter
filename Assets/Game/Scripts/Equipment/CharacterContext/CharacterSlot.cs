using System.Collections.Generic;
using Game.Scripts.Equipment.CharacterContext.DragInDrop;
using Game.Scripts.Equipment.DragInDrop;
using Game.Scripts.MV.StatContext.Type;
using UnityEngine;

namespace Game.Scripts.Equipment.CharacterContext
{
    public class CharacterSlot : Slot
    {
        private readonly Dictionary<StatType, int> _stats = new();
        
        [field: SerializeField] public CharacterDragSlot CharacterDrag { get; private set; }
        
        public Character Character { get; private set; }
        public IReadOnlyDictionary<StatType, int> Stats => _stats;

        public void Initialize(Character character, CharacterStat[] stats)
        {
            Character = character;

            foreach (var stat in stats)
                _stats.Add(stat.Type, stat.Value);

            GetDrag();
        }

        protected override DragSlot GetDrag()
        {
            CharacterDrag.Initialize(this);
            return CharacterDrag;
        }
    }
}