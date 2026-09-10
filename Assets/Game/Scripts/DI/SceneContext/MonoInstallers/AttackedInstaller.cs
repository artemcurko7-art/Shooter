using Game.Scripts.Attacked;
using Game.Scripts.Service.Subscriber;
using Game.Scripts.Service.Weapon;
using UnityEngine;
using Zenject;

namespace Game.Scripts.DI.SceneContext.MonoInstallers
{
    public class AttackedInstaller : MonoInstaller
    {
        private WeaponService _weaponService;
        private Transform[] _handGrips;
        
        [Inject]
        public void Construct(WeaponService weaponService)
        {
            _weaponService = weaponService;
        }
        
        public override void InstallBindings()
        {
            Container
                .Bind<ISubscriber>()
                .To<AnimationRecoil>()
                .AsCached()
                .WithArguments(_handGrips = new [] { _weaponService.View.LeftHandGrip, _weaponService.View.RightHandGrip } );
        }
    }
}