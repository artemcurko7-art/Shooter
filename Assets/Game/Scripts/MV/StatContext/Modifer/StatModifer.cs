using System;
using System.Collections.Generic;
using Game.Scripts.MV.StatContext.Repository;
using Game.Scripts.MV.StatContext.Type;

namespace Game.Scripts.MV.StatContext.Modifer
{
    public class StatModifer : IStatModifer
    {
        private readonly Dictionary<StatType, int> _stats = new();

        public StatModifer()
        {
            Fill();
        }
        
        public IReadOnlyDictionary<StatType, int> Stats => _stats;

        private void Fill()
        {
            foreach (var type in Enum.GetValues(typeof(StatType)))
            {
                if ((StatType)type == StatType.None)
                    continue;
                
                _stats.Add((StatType)type, 0);
            }
        }
        
        public void Set(StatType type, int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException($"Value: {value} zero");

            _stats[type] = value;
        }
    }
}