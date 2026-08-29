using Game.Scripts.Equipment;
using Game.Scripts.Equipment.Data;
using Game.Scripts.Equipment.DragInDrop;
using Game.Scripts.Equipment.Handler;
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
        [SerializeField] private DisplayReplacement[] _tabReplacements;
        [SerializeField] private DisplayStat _displayStat;
        [SerializeField] private Slot _slot;
        [SerializeField] private Transform _equipmentContainer;
        [SerializeField] private Transform _replacementContainer;
        [SerializeField] private GridLayoutGroup _gridLayoutGroup;
        
        public override void InstallBindings()
        {
            Bind();
            BindData();
            BindReplacement();
            BindHandler();
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
                .WithArguments(_dropSlots, _gridLayoutGroup);
            
            Container
                .Bind<ISubscriber>()
                .To<ReplacementService>()
                .AsSingle()
                .WithArguments(_dropSlots, _replacementContainer);

            Container
                .Bind<DisplayStatFactory>()
                .AsSingle()
                .WithArguments(_displayStat);
            
            Container
                .Bind<ISubscriber>()
                .To<TabOpened>()
                .AsSingle()
                .WithArguments(_tabReplacements);
        }

        private void BindHandler()
        {
            Container
                .BindInterfacesAndSelfTo<SlotHandler>()
                .AsSingle()
                .WithArguments(_dropSlots);
            
            Container
                .Bind<ISubscriber>()
                .To<WeaponSlotHandler>()
                .AsSingle()
                .WithArguments(_dropSlots[0]);
        }
    }
}