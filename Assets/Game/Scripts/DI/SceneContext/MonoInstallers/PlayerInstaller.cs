using Game.Scripts.PlayerContext;
using Game.Scripts.PlayerContext.Input;
using Game.Scripts.Provider;
using Game.Scripts.WeaponContext;
using UnityEngine;
using YG;
using Zenject;

namespace Game.Scripts.DI.SceneContext.MonoInstallers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Player _player;
        [SerializeField] private FixedJoystick _joystick;

        private WeaponProvider _weaponProvider;
    
        [Inject]
        public void Construct(WeaponProvider weaponProvider)
        {
            _weaponProvider = weaponProvider;
        }
    
        public override void InstallBindings()
        {
            if (YG2.envir.isMobile)
            {
                Container
                    .Bind<IInput>()
                    .To<MobileInput>()
                    .AsSingle()
                    .WithArguments(_joystick);
            }
            else
            {
                Container
                    .Bind<IInput>()
                    .To<DesktopInput>()
                    .AsSingle();
            }
        
            Container
                .Bind<Mover>()
                .AsSingle();
        
            Container
                .Bind<Rotation>()
                .AsSingle();
        
            Container
                .Bind<ITransformable>()
                .FromInstance(_player)
                .AsSingle();

            Container
                .Bind<TrackerUnits>()
                .AsSingle();
        
            Container
                .Bind<Weapon>()
                .AsSingle();

            Container
                .BindInstance(_weaponProvider.Type)
                .AsSingle();
        
            Container
                .Bind<RotationToTarget>()
                .AsSingle();
        
            Container
                .Bind<CalculationRotationAngle>()
                .AsSingle();
        }
    }
}