using System;
using Game.Scripts.MV.StatContext.Type;
using UnityEngine;

namespace Game.Scripts.MV.StatContext
{
    public abstract class Stat : IStat
    {
        private readonly int _defaultValue;
        private int _value;
        
        public event Action<int> ValueChanged;
        
        public Stat(int value, bool isPercentageValue)
        {
            _defaultValue = value;
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

        public void Update()
        {
            ValueChanged?.Invoke(Value);
        }

        public virtual void Increase(int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            
            Value += value;
        }
        
        public void Decrease(int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            
            Value -= value;
        }

        public void SetDefaultValue()
        {
            Value = _defaultValue;
        }
        
        protected abstract StatType GetStatType();
    }
}