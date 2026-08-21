using Game.Scripts.DragInDrop;
using Game.Scripts.Equipment;
using Game.Scripts.Equipment.Data;
using Game.Scripts.Factory;
using Game.Scripts.Service.Equipment;
using Game.Scripts.Service.Subscriber;
using UnityEngine;
using Zenject;

namespace Game.Scripts.DI.SceneContext.MonoInstallers
{
    public class EquipmentInstaller : MonoInstaller
    {
        [SerializeField] private DropSlot[] _dropSlots;
        [SerializeField] private Slot _slot;
        [SerializeField] private Transform _container;
        
        public override void InstallBindings()
        {
            Container
                .Bind<EquipmentData>()
                .AsSingle();
            
            Container
                .Bind<RarityEquipmentData>()
                .AsSingle();
            
            Container
                .Bind<SlotFactory>()
                .AsSingle()
                .WithArguments(_slot);

            Container
                .Bind<SortingEquipmentByParameters>()
                .AsSingle()
                .WithArguments(_container);

            Container
                .Bind<ISubscriber>()
                .To<EquipmentService>()
                .AsSingle()
                .WithArguments(_dropSlots, _container)
                .NonLazy();
        }
    }
}