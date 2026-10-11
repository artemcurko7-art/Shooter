using UnityEngine;

namespace Game.Scripts.WeaponContext
{
    public class CriticalDamageResolver
    {
        private readonly float _baseChance;
        private float _currentChance;
        private int _missStreak;

        public CriticalDamageResolver(float criticalChance)
        {
            _baseChance = Mathf.Clamp01(criticalChance / 100);
            _currentChance = _baseChance;
        }

        public bool Resolve()
        {
            if (_currentChance >= 1f)
            {
                Reset();
                
                return true;
            }
            
            if (_currentChance <= 0f) 
                return false;

            if (UserUtils.NumberGeneration.GetFloatRandom(0, 1) < _currentChance)
            {
                Reset();
                return true;
            }

            _missStreak++;
            _currentChance = _baseChance * (_missStreak + 1);
            return false;
        }

        private void Reset()
        {
            _missStreak = 0;
            _currentChance = _baseChance;
        }
    }
}