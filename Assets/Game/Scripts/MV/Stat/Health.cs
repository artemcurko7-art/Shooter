using System;

namespace Game.Scripts.MV.Stat
{
    public class Health
    {
        private int _value;
        private int _maxValue;

        public Health()
        {
            _value = 100;
        }
    
        public event Action<int> Changed;

        public int Value
        {
            get => _value;

            private set
            {
                _value = Math.Clamp(value, 0, _maxValue);
                Changed?.Invoke(_value);
            }
        }

        public void TakeDamage(int damage)
        {
            Value -= damage;
        }

        public void Heal(int amount)
        {
            Value += amount;
        }
    }
}