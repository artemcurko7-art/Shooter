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
        [field: SerializeField] public CharacterDragSlot CharacterDrag { get; private set; }

        public CharacterType Type { get; private set; }
        public IReadOnlyDictionary<StatType, Stat> Stats { get; private set; }

        public void Initialize(CharacterType type, IReadOnlyDictionary<StatType, Stat> stats)
        {
            Type = type;
            Stats = stats;
        }

        protected override DragSlot GetDrag()
        {
            CharacterDrag.Initialize(this);
            return CharacterDrag;
        }
    }
}