using System.Collections.Generic;
using Game.Scripts.Equipment.CharacterContext.DragInDrop;
using Game.Scripts.Equipment.CharacterContext.Type;
using Game.Scripts.Equipment.DragInDrop;
using Game.Scripts.MV.StatContext.Type;
using UnityEngine;

namespace Game.Scripts.Equipment.CharacterContext
{
    public class CharacterSlot : Slot
    {
        private readonly Dictionary<StatType, int> _stats = new();
        
        [field: SerializeField] public CharacterDragSlot CharacterDrag { get; private set; }
        
        public IReadOnlyDictionary<StatType, int> Stats => _stats;

        public CharacterType Type { get; private set; }

        public void Initialize(CharacterType type, CharacterStat[] stats)
        {
            Type = type;

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