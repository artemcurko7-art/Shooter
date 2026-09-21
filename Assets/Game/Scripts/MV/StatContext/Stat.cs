using System;
using Game.Scripts.MV.StatContext.Type;
using UnityEngine;

namespace Game.Scripts.MV.StatContext
{
    public abstract class Stat
    {
        private int _value;
        
        public event Action<float> ValueChanged;
        
        public Stat(int value, bool isPercentageValue)
        {
            _value = value;
            IsPercentageValue = isPercentageValue;
        }

        public StatType Type => GetStatType();
        public bool IsPercentageValue { get; }
            
        public int Value
        {
            get => _value;

            private set
            {
                _value = Mathf.Clamp(value, 0, int.MaxValue);
                ValueChanged?.Invoke(_value);
            }
        }

        public virtual void Increase(int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));
            
            Value += amount;
        }
        
        public virtual void Decrease(int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));
            
            Value -= amount;
        }

        protected abstract StatType GetStatType();
    }
}