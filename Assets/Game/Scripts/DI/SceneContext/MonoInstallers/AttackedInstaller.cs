using Game.Scripts.Attacked;
using Game.Scripts.Service.Subscriber;
using Game.Scripts.Service.Weapon;
using UnityEngine;
using Zenject;

namespace Game.Scripts.DI.SceneContext.MonoInstallers
{
    public class AttackedInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<ISubscriber>()
                .To<AnimationRecoilAttackable>()
                .AsCached();
        }
    }
}