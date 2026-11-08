using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.PassiveAbilityContext.AttributeContext;
using Game.Scripts.PassiveAbilityContext.Data;
using Game.Scripts.PassiveAbilityContext.Type;
using UnityEngine;

namespace Game.Scripts.PassiveAbilityContext.Passive
{
    [PassiveAbilityType(PassiveAbilityType.Temporary)]
    public class TemporaryPassiveAbility : IPassiveAbility
    {
        private readonly StatType _type;
        private readonly int _value;
        private readonly float _cooldown;
        
        public event Action<ICharacter, PassiveAbilityData> Applied;

        public TemporaryPassiveAbility(StatType type, int value, float cooldown)
        {
            _type = type;
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
                
                character.Stats[_type].Increase(_value);

                Applied?.Invoke(character, passiveAbilityData);
                
                await UniTask.Delay(TimeSpan.FromSeconds(_cooldown), cancellationToken: token);
                
                character.Stats[_type].Decrease(_value);
            }
        }
    }
}