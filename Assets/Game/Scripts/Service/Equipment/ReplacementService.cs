using System.Collections.Generic;
using Game.Scripts.Equipment;
using Game.Scripts.Equipment.Data;
using Game.Scripts.Equipment.DragInDrop;
using Game.Scripts.Equipment.Replacement;
using Game.Scripts.Equipment.Repository;
using Game.Scripts.Equipment.Type;
using Game.Scripts.Factory;
using Game.Scripts.MV.StatContext;
using UnityEngine;

namespace Game.Scripts.Service.Equipment
{
    public class ReplacementService : SlotProcessor, IReplacementService
    {
        private readonly ITabService _tabService;
        private readonly DisplayStatData _displayStatData;
        private readonly DisplayStatFactory _displayStatFactory;
        private readonly List<DisplayStat> _displayStats = new();
        private readonly ReplacementContainer _container;
        
        public ReplacementService(
            IEquipmentService equipmentService,
            EquipmentSlotRepository repository,
            EquipmentFreeSlotRegistry freeRegistry,
            SortingEquipmentByParameters sorting,
            DropSlot[] dropSlots,
            ITabService tabService, 
            DisplayStatData displayStatData,
            DisplayStatFactory displayStatFactory,
            ReplacementContainer container) : base(equipmentService, repository, freeRegistry, sorting, dropSlots)
        {
            _tabService = tabService;
            _displayStatData = displayStatData;
            _displayStatFactory = displayStatFactory;
            _container = container;
        }

        public Slot Slot => DraggedSlot;
        
        public override void Subscribe()
        {
            base.Subscribe();
            
            _tabService.TabOpened += OnTabOpened;
        }
        
        public override void Unsubscribe()
        {
            base.Unsubscribe();
            
            _tabService.TabOpened -= OnTabOpened;
        }
        
        private void OnTabOpened(bool isActive)
        {
            if (isActive)
            {
                _container.Select(ReplacementType.From);
                CreateDisplayStat(FreeRegistry.EquippedSlots[DroppedSlot.EquipmentItem.Type], _container.MainContainer, _container.AdditionalContainer);
                _container.Select(ReplacementType.To);
                CreateDisplayStat(DraggedSlot, _container.MainContainer, _container.AdditionalContainer);
            }
            else
            {
                foreach (var stat in _displayStats)
                    stat.OnDestroyed();

                _displayStats.Clear();
                
                foreach (var dropSlot in DropSlots)
                    if (dropSlot.EquipmentType == DroppedSlot.EquipmentItem.Type)
                        FreeRegistry.Register(dropSlot.EquipmentType, DroppedSlot);
            }
        }

        private void CreateDisplayStat(Slot slot, Transform mainContainer, Transform additionalContainer)
        {
            Stat mainDraggedStat = slot.EquipmentItem.MainStat;

            _displayStats.Add(_displayStatFactory.Create(_displayStatData.Stats[mainDraggedStat.Type], mainContainer,
                (int)mainDraggedStat.Value, mainDraggedStat.IsPercentageValue));
                
            foreach (var stat in slot.EquipmentItem.AdditionalStats)
            {
                _displayStats.Add(_displayStatFactory.Create(_displayStatData.Stats[stat.Type], additionalContainer,
                    (int)stat.Value, stat.IsPercentageValue));
            }
        }
    }
}