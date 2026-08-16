using Game.Scripts.Service.Equipment;
using UnityEngine;
using Zenject;

namespace Game.Scripts.DI.SceneContext.MonoInstallers
{
    public class EquipmentInstaller : MonoInstaller
    {
        [SerializeField] private Transform _container;
        
        public override void InstallBindings()
        {
            Container
                .Bind<EquipmentService>()
                .AsSingle()
                .WithArguments(_container)
                .NonLazy();
        }
    }
}