using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.MV.StatContext;
using Game.Scripts.MV.StatContext.Modifer;
using Game.Scripts.PassiveAbilityContext.AttributeContext;
using Game.Scripts.PassiveAbilityContext.Data;
using Game.Scripts.PassiveAbilityContext.Type;
using UnityEngine;

namespace Game.Scripts.PassiveAbilityContext.Passive
{
    [PassiveAbilityType(PassiveAbilityType.RegenerationHealth)]
    public class RegenerationHealthPassiveAbility : IPassiveAbility
    {
        private readonly Health _health;
        private readonly StatModifer _statModifer;
        private readonly int _value;
        private readonly float _cooldown;
        
        public event Action<ICharacter, PassiveAbilityData> Applied;
    
        public RegenerationHealthPassiveAbility(Health health, StatModifer statModifer, int value, float cooldown)
        {
            _health = health;
            _statModifer = statModifer;
            _value = value;
            _cooldown = cooldown;
        }
    
        public void Apply(ICharacter character, PassiveAbilityData passiveAbilityData)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            StartAsync(character, passiveAbilityData, cancellationTokenSource.Token).Forget();
        }
    
        private async UniTaskVoid StartAsync(ICharacter character, PassiveAbilityData passiveAbilityData, CancellationToken token)
        {
            while (token.IsCancellationRequested == false)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_cooldown), cancellationToken: token);
    
                int value = GetValue();
                
                if (value == 0)
                    continue;
                
                _health.Increase(value);
                _statModifer.Set(_health.Type, value);
                Applied?.Invoke(character, passiveAbilityData);
                
                Debug.Log("Regeneration Health Passive Ability");
            }
        }
        
        private int GetValue()
        {
            int currentValue = 0;
            
            int calculationValue = _health.MaxValue - _health.Value;
            
            if (_value < calculationValue)
                return _value;
            
            if (_value > calculationValue && calculationValue != 0)
                return calculationValue;
            
            return currentValue;
        }
    }
}