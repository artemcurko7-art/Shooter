using System.Collections.Generic;
using Game.Scripts.MV.StatContext;
using Game.Scripts.MV.StatContext.Type;
using UnityEngine;

namespace Game.Scripts.Common.Interfaces
{
    public interface IAttacker
    {
        public IReadOnlyDictionary<StatType, Stat> Stats { get; }
        public Transform Shoot { get; }
        public Transform Transform { get; }
    }
}