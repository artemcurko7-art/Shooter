using Game.Scripts.PlayerContext;
using Game.Scripts.PlayerContext.GameInput;
using UnityEngine;
using YG;
using Zenject;

namespace Game.Scripts.DI.SceneContext.MonoInstallers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private LayerMask _layerMaskUnit;
        [SerializeField] private Player _player;
        [SerializeField] private FixedJoystick _joystick;
        [SerializeField] private float _radius;
    
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
                .AsSingle()
                .WithArguments(_layerMaskUnit, _radius);
            
            Container
                .Bind<RotationToTarget>()
                .AsSingle();
        
            Container
                .Bind<CalculationRotationAngle>()
                .AsSingle();
        }
    }
}