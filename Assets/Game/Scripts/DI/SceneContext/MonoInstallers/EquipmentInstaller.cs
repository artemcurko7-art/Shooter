using Game.Scripts.Equipment;
using Game.Scripts.Equipment.Data;
using Game.Scripts.Equipment.DragInDrop;
using Game.Scripts.Equipment.Replacement;
using Game.Scripts.Factory;
using Game.Scripts.Service.Equipment;
using Game.Scripts.Service.Subscriber;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.DI.SceneContext.MonoInstallers
{
    public class EquipmentInstaller : MonoInstaller
    {
        [SerializeField] private DropSlot[] _dropSlots;
        [SerializeField] private TabReplacement[] _tabReplacements;
        [SerializeField] private Slot _slot;
        [SerializeField] private Transform _container;
        [SerializeField] private GridLayoutGroup _gridLayoutGroup;
        
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
                .BindInterfacesAndSelfTo<EquipmentService>()
                .AsSingle()
                .WithArguments(_container)
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<SlotHandler>()
                .AsSingle()
                .WithArguments(_dropSlots);
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
                .WithArguments(_tabReplacements);

            Container
                .BindInterfacesAndSelfTo<ReplacementController>()
                .AsSingle()
                .WithArguments(_dropSlots, _gridLayoutGroup);
        }
    }
}