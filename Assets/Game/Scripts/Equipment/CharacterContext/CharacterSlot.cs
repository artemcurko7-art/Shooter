using System;
using System.Collections.Generic;
using Game.Scripts.Equipment.CharacterContext.DragInDrop;
using Game.Scripts.Equipment.CharacterContext.Type;
using Game.Scripts.Equipment.DragInDrop;
using Game.Scripts.MV.StatContext;
using Game.Scripts.MV.StatContext.Repository;
using Game.Scripts.MV.StatContext.Type;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Equipment.CharacterContext
{
    public class CharacterSlot : Slot
    {
        private readonly Dictionary<StatType, Stat> _stats = new();
        
        [field: SerializeField] public CharacterDragSlot CharacterDrag { get; private set; }
        
        private IStatTypeRegistry _registry;

        public CharacterType Type { get; private set; }
        public IReadOnlyDictionary<StatType, Stat> Stats => _stats;

        [Inject]
        public void Construct(IStatTypeRegistry registry)
        {
            _registry = registry;
        }

        public void Initialize(CharacterType type, CharacterStat[] stats)
        {
            Type = type;
            
            foreach (var stat in stats)
                if (_registry.Types.TryGetValue(stat.Type, out var statType))
                    _stats.Add(stat.Type, (Stat)Activator.CreateInstance(statType, stat.Value, false));
        }

        protected override DragSlot GetDrag()
        {
            CharacterDrag.Initialize(this);
            return CharacterDrag;
        }
    }
}