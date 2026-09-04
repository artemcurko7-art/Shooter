using Game.Scripts.Equipment;
using Game.Scripts.Equipment.Data;
using Game.Scripts.Equipment.DragInDrop;
using Game.Scripts.Equipment.Handler;
using Game.Scripts.Equipment.Replacement;
using Game.Scripts.Equipment.Repository;
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
        [SerializeField] private Slot _slot;
        [SerializeField] private DisplayReplacement _displayReplacement;
        [SerializeField] private DisplayStat _displayStat;
        [SerializeField] private ReplacementStatContainer _statContainer;
        [SerializeField] private DropSlot[] _dropSlots;
        [SerializeField] private Transform _equipmentContainer;
        [SerializeField] private GridLayoutGroup _gridLayoutGroup;
        
        public override void InstallBindings()
        {
            Bind();
            BindData();
            BindReplacement();
            BindHandler();
            BindRepository();
        }

        private void Bind()
        {
            Container
                .Bind<EquipmentSlotFactory>()
                .AsSingle()
                .WithArguments(_slot);

            Container
                .Bind<SortingEquipmentByParameters>()
                .AsSingle()
                .WithArguments(_equipmentContainer);

            Container
                .BindInterfacesAndSelfTo<EquipmentService>()
                .AsSingle()
                .WithArguments(_equipmentContainer)
                .NonLazy();

            Container
                .Bind<DropSlot[]>()
                .FromInstance(_dropSlots)
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

        private void BindReplacement()
        {
            Container
                .Bind<DisplayStatData>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<ReplacementController>()
                .AsSingle()
                .WithArguments(_gridLayoutGroup);
            
            Container
                .BindInterfacesTo<ReplacementService>()
                .AsSingle()
                .WithArguments(_statContainer);

            Container
                .Bind<DisplayStatFactory>()
                .AsSingle()
                .WithArguments(_displayStat);
            
            Container
                .Bind<ISubscriber>()
                .To<TabOpened>()
                .AsSingle()
                .WithArguments(_displayReplacement);
            
            Container
                .Bind<ComparisonStat>()
                .AsSingle();
        }

        private void BindHandler()
        {
            Container
                .BindInterfacesAndSelfTo<SlotHandler>()
                .AsSingle();
            
            Container
                .Bind<ISubscriber>()
                .To<WeaponSlotHandler>()
                .AsSingle()
                .WithArguments(_dropSlots[0]);
        }

        private void BindRepository()
        {
            Container
                .Bind<EquipmentSlotRepository>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<EquipmentFreeSlotRegistry>()
                .AsSingle();
        }
    }
}