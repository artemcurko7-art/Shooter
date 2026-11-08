using System.Collections.Generic;
using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.Extensions;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.PassiveAbilityContext.Data;
using Game.Scripts.PassiveAbilityContext.Repository;
using Zenject;

namespace Game.Scripts.PoolMono.Repository.PassiveAbilityView
{
    public class PassiveAbilityViewPoolRepository : IPassiveAbilityViewPoolRepository
    {
        private readonly Dictionary<PassiveAbilityData, PassiveAbilityViewPool> _pools = new();
        
        public PassiveAbilityViewPoolRepository(
            ICharacterStatResolver characterStatResolver,
            IPassiveAbilityDataRepository repository,
            DiContainer container)
        {
            foreach (var character in characterStatResolver.Characters)
            {
                var passiveAbilityData = repository.PassiveAbilityDates[character.Type];
                var pool = container.InstantiatePassiveAbilityViewPool(passiveAbilityData, character.Transform);

                _pools.Add(passiveAbilityData, pool);
            }
        }
        
        public IReadOnlyDictionary<PassiveAbilityData, PassiveAbilityViewPool> Pools => _pools;
    }
}