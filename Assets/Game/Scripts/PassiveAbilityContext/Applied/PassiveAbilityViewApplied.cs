using Game.Scripts.Equipment.CharacterContext;
using Game.Scripts.PassiveAbilityContext.Data;
using Game.Scripts.PoolMono.Repository.PassiveAbilityView;
using Game.Scripts.Service.PassiveAbilityContext;

namespace Game.Scripts.PassiveAbilityContext.Applied
{
    public class PassiveAbilityViewApplied : AppliedObserver
    {
        private readonly IPassiveAbilityViewPoolRepository _repository;
        
        public PassiveAbilityViewApplied(IPassiveAbilityViewPoolRepository repository, IPassiveAbilityService passiveAbilityService) : base(passiveAbilityService)
        {
            _repository = repository;
        }

        protected override void OnApplied(ICharacter character, PassiveAbilityData passiveAbilityData)
        {
            var pool = _repository.Pools[passiveAbilityData];
            pool.SetTransform(character.Transform);
            pool.Get();
        }
    }
}