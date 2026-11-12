using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Scripts.Common.Interfaces;
using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.PassiveAbilityContext.Data;

namespace Game.Scripts.PassiveAbilityContext.Passive
{
    public class TemporaryPassiveAbility : IPassiveAbility
    {
        private readonly StatType _type;
        private readonly int _value;
        private readonly float _cooldown;
        
        public event Action<IAttacker, PassiveAbilityData> Applied;

        public TemporaryPassiveAbility(StatType type, int value, float cooldown)
        {
            _type = type;
            _value = value;
            _cooldown = cooldown;
        }

        public void Apply(IAttacker attacker, PassiveAbilityData passiveAbilityData)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            StartAsync(attacker, passiveAbilityData, cancellationTokenSource.Token).Forget();
        }

        private async UniTaskVoid StartAsync(IAttacker attacker, PassiveAbilityData passiveAbilityData, CancellationToken token)
        {
            while (token.IsCancellationRequested == false)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_cooldown), cancellationToken: token);
                
                attacker.Stats[_type].Increase(_value);

                Applied?.Invoke(attacker, passiveAbilityData);
                
                await UniTask.Delay(TimeSpan.FromSeconds(_cooldown), cancellationToken: token);
                
                attacker.Stats[_type].Decrease(_value);
            }
        }
    }
}