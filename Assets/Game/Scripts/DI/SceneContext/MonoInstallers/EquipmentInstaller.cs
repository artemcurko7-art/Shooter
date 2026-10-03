using Game.Scripts.Equipment;
using Game.Scripts.Equipment.CharacterContext.Handler;
using Game.Scripts.Equipment.CharacterContext.Replacement;
using Game.Scripts.Equipment.EquipmentContext;
using Game.Scripts.Equipment.EquipmentContext.Data;
using Game.Scripts.Equipment.EquipmentContext.DragInDrop;
using Game.Scripts.Equipment.EquipmentContext.Handler;
using Game.Scripts.Equipment.EquipmentContext.Replacement;
using Game.Scripts.Equipment.EquipmentContext.Repository;
using Game.Scripts.Equipment.EquipmentContext.Type;
using Game.Scripts.Equipment.Repository;
using Game.Scripts.Factory;
using Game.Scripts.Service.Equipment;
using Game.Scripts.Service.Equipment.EquipmentContext;
using Game.Scripts.Service.Equipment.Reward;
using Game.Scripts.Service.Subscriber;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.DI.SceneContext.MonoInstallers
{
    public class EquipmentInstaller : MonoInstaller
    {
        [SerializeField] private EquipmentSlot _slot;
        [SerializeField] private EquipmentDisplayReplacement _displayReplacement;
        [SerializeField] private DisplayStat _displayStat;
        [SerializeField] private ReplacementStatContainer _statContainer;
        [SerializeField] private EquipmentDropSlot[] _dropSlots;
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
                .BindInterfacesAndSelfTo<EquipmentSlotRewardService>()
                .AsSingle()
                .WithArguments(_equipmentContainer)
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<GeneralStatsHandler>()
                .AsSingle();
            
            Container
                .Bind<EquipmentSlotFactory>()
                .AsSingle()
                .WithArguments(_slot);

            Container
                .Bind<SortingEquipmentByParameters>()
                .AsSingle()
                .WithArguments(_equipmentContainer);

            Container
                .Bind<EquipmentDropSlot[]>()
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
                .BindInterfacesTo<EquipmentReplacementController>()
                .AsSingle()
                .WithArguments(_gridLayoutGroup);
            
            Container
                .BindInterfacesTo<EquipmentReplacementService>()
                .AsSingle()
                .WithArguments(_statContainer);

            Container
                .Bind<DisplayStatFactory>()
                .AsSingle()
                .WithArguments(_displayStat);
            
            Container
                .Bind<ISubscriber>()
                .To<EquipmentReplacementTabOpened>()
                .AsSingle()
                .WithArguments(_displayReplacement);
            
            Container
                .Bind<ComparisonStat>()
                .AsSingle();
        }

        private void BindHandler()
        {
            Container
                .BindInterfacesAndSelfTo<EquipmentSlotHandler>()
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
                .Bind<SlotRepository<EquipmentSlot>>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<FreeSlotRegistry<EquipmentType, EquipmentSlot>>()
                .AsSingle();
        }
    }
}