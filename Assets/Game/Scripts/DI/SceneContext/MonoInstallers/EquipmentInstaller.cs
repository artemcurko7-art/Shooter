using Game.Scripts.Equipment;
using Game.Scripts.Equipment.Data;
using Game.Scripts.Equipment.DragInDrop;
using Game.Scripts.Equipment.Replacement;
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
        [SerializeField] private TabReplacement[] _exchanges;
        [SerializeField] private Slot _slot;
        [SerializeField] private Transform _container;
        
        public override void InstallBindings()
        {
            Bind();
            BindData();
            BindExchange();
        }

        private void Bind()
        {
            Container
                .Bind<SlotFactory>()
                .AsSingle()
                .WithArguments(_slot);

            Container
                .Bind<SortingEquipmentByParameters>()
                .AsSingle()
                .WithArguments(_container);

            Container
                .BindInterfacesTo<EquipmentService>()
                .AsSingle()
                .WithArguments(_dropSlots, _container);

            Container
                .Bind<SlotOccupancyService>()
                .AsSingle();
        }
        
        private void BindData()
        {
            Container
                .Bind<EquipmentData>()
                .AsSingle();
            
            Container
                .Bind<RarityEquipmentData>()
                .AsSingle();
        }

        private void BindExchange()
        {
            Container
                .Bind<ISubscriber>()
                .To<TabOpened>()
                .AsSingle()
                .WithArguments(_dropSlots, _exchanges);

            Container
                .BindInterfacesAndSelfTo<ReplacementController>()
                .AsSingle()
                .WithArguments(_dropSlots);
        }
    }
}