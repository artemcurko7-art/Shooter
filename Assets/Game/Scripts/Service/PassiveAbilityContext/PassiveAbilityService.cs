using System.Collections.Generic;
using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.Factory;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.PassiveAbilityContext;
using Game.Scripts.PoolMono;
using Game.Scripts.PoolMono.Repository.Effect;
using Game.Scripts.Service.Subscriber;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Service.PassiveAbilityContext
{
    public class PassiveAbilityService : ISubscriber
    {
        private readonly ICharacterStatResolver _characterStatResolver;
        private readonly PassiveAbilityFactory _factory;
        private readonly DiContainer _container;
        //private readonly PassiveAbilityViewPool[] _pools;
        private readonly PassiveAbilityViewFactory _viewFactory;
        private readonly List<PassiveAbility> _effects = new();
        private readonly Dictionary<StatType, PassiveAbilityViewPool> _pools = new();
        
        public PassiveAbilityService(
            ICharacterStatResolver characterStatResolver,
            PassiveAbilityFactory factory,
            PassiveAbilityViewFactory viewFactory,
            DiContainer container)
            //PassiveAbilityViewPool[] pools)
        {
            _characterStatResolver = characterStatResolver;
            _factory = factory;
            _viewFactory = viewFactory;
            _container = container;

            // foreach (var character in _characterStatResolver.Characters)
            // {
            //     var pool = _container.Instantiate<PassiveAbilityViewPool>(new object[] {character.PassiveAbilityData, character.Transform});
            //     
            //     _pools.Add(character.PassiveAbilityData.StatType, pool);
            // }
            
            Fill();
        }

        public void Subscribe()
        {
            foreach (var passiveAbility in _effects)
                passiveAbility.Effect.Applied += OnApplied;

            for (int i = 0; i < _characterStatResolver.Characters.Count; i++) // test
            {
                _effects[i].Apply(_characterStatResolver.Characters[i], _characterStatResolver.Characters);
            }
        }

        public void Unsubscribe()
        {
            foreach (var passiveAbility in _effects)
                passiveAbility.Effect.Applied -= OnApplied;
        }
        
        private void Fill()
        {
            foreach (var character in _characterStatResolver.Characters)
            {
                var passiveAbility = _factory.Create(character.PassiveAbilityData);
                //passiveAbility.Apply(character, _characterStatResolver.Characters);
                _effects.Add(passiveAbility);
            }
        }

        private void OnApplied(ICharacter character)
        {
            _viewFactory.Create(character.PassiveAbilityData, character.Transform);

            // foreach (var pool in _pools)
            //     pool.Get();

            //var passive = _pools[character.PassiveAbilityData.StatType].Get();
            //Debug.Log($"Passive: {_pools[character.PassiveAbilityData.StatType] == null}");
            
            Debug.Log("On Applied");
        }
    }
}