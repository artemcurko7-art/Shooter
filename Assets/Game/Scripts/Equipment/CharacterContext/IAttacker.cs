using System.Collections.Generic;
using Game.Scripts.Equipment.CharacterContext.Type;
using Game.Scripts.MV.StatContext;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.SquadContext.Type;
using Game.Scripts.WeaponContext;
using UnityEngine;

namespace Game.Scripts.Equipment.CharacterContext
{
    public interface IAttacker
    {
        public IReadOnlyDictionary<StatType, Stat> Stats { get; }
        public Transform Transform { get; }
    }
}