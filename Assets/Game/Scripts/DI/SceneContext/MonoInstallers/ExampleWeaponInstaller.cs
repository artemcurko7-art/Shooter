using Game.Scripts.Service.Equipment.CharacterContext;
using Game.Scripts.WeaponContext.Shooting;
using Zenject;

namespace Game.Scripts.DI.SceneContext.MonoInstallers
{
    public class ExampleWeaponInstaller : MonoInstaller
    {
        private ICharacterService _characterService;
        
        [Inject]
        public void Construct(ICharacterService characterService)
        {
            _characterService = characterService;
        }
        
        public override void InstallBindings()
        {
            foreach (var weaponView in _characterService.WeaponViews)
            {
                Container
                    .Bind<IWeaponShooting>()
                    .FromInstance(weaponView.Weapon.Shooting)
                    .AsCached();
            }
        }
    }
}