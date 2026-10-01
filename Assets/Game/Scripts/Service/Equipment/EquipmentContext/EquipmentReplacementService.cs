using System.Collections.Generic;
using Game.Scripts.Equipment;
using Game.Scripts.Equipment.EquipmentContext;
using Game.Scripts.Equipment.EquipmentContext.Data;
using Game.Scripts.Equipment.EquipmentContext.DragInDrop;
using Game.Scripts.Equipment.EquipmentContext.Handler;
using Game.Scripts.Equipment.EquipmentContext.Replacement;
using Game.Scripts.Equipment.EquipmentContext.Type;
using Game.Scripts.Equipment.Repository;
using Game.Scripts.Factory;
using Game.Scripts.MV.StatContext;
using UnityEngine;

namespace Game.Scripts.Service.Equipment.EquipmentContext
{
    public class EquipmentReplacementService : EquipmentSlotProcessor, IReplacementService
    {
        private readonly ITabService<EquipmentSlotHandler> _tabService;
        private readonly DisplayStatData _displayStatData;
        private readonly DisplayStatFactory _displayStatFactory;
        private readonly ComparisonStat _comparisonStat;
        private readonly ReplacementStatContainer _statContainer;
        private List<DisplayStat> _displayStatDraggables;
        private List<DisplayStat> _displayStatDroppables;
        private DisplayStat _displayStatDragged;
        private DisplayStat _displayStatDropped;
        private bool _isReopening;

        public EquipmentReplacementService(
            IEquipmentService equipmentService,
            SlotRepository<EquipmentSlot> repository,
            FreeSlotRegistry<EquipmentType, EquipmentSlot> freeRegistry,
            EquipmentDropSlot[] dropSlots,
            SortingEquipmentByParameters sorting,
            ITabService<EquipmentSlotHandler> tabService,
            DisplayStatData displayStatData,
            DisplayStatFactory displayStatFactory,
            ComparisonStat comparisonStat,
            ReplacementStatContainer statContainer)
            : base(equipmentService, repository, freeRegistry, dropSlots, sorting)
        {
            _tabService = tabService;
            _displayStatData = displayStatData;
            _displayStatFactory = displayStatFactory;
            _comparisonStat = comparisonStat;
            _statContainer = statContainer;
        }

        public EquipmentSlot EquipmentSlot => DraggedSlot;
        
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
            if (isActive && _isReopening == false)
            {
                _statContainer.Select(ReplacementType.From);
                CreateDisplayStat(out DisplayStat displayStatDragged, out List<DisplayStat> displayStatDraggables, FreeRegistry.EquippedSlots[DroppedSlot.EquipmentItem.Type], _statContainer.MainContainer, _statContainer.AdditionalContainer);
                _statContainer.Select(ReplacementType.To);
                CreateDisplayStat(out DisplayStat displayStatDropped, out List<DisplayStat> displayStatDroppables, DraggedSlot, _statContainer.MainContainer, _statContainer.AdditionalContainer);
                
                _displayStatDragged = displayStatDragged;
                _displayStatDropped = displayStatDropped;
                _displayStatDraggables = displayStatDraggables;
                _displayStatDroppables = displayStatDroppables;

                _comparisonStat.CompareMain(displayStatDragged, displayStatDropped);
                _comparisonStat.CompareAdditional(displayStatDraggables, _displayStatDroppables);

                _isReopening = true;
            }
            else
            {
                foreach (var displayStat in _displayStatDraggables)
                    displayStat.OnDestroyed();
                
                foreach (var displayStat in _displayStatDroppables)
                    displayStat.OnDestroyed();

                _displayStatDragged.OnDestroyed();
                _displayStatDropped.OnDestroyed();
                
                _displayStatDraggables.Clear();
                _displayStatDroppables.Clear();
                
                foreach (var dropSlot in DropSlots)
                    if (dropSlot.EquipmentType == DroppedSlot.EquipmentItem.Type)
                        FreeRegistry.Register(dropSlot.EquipmentType, DroppedSlot);

                _isReopening = false;
            }
        }

        private void CreateDisplayStat(out DisplayStat displayStat, out List<DisplayStat> displayStats, EquipmentSlot slot, Transform mainContainer, Transform additionalContainer)
        {
            displayStats = new();
            
            Stat mainDraggedStat = slot.EquipmentItem.MainStat;

            displayStat = _displayStatFactory.Create(_displayStatData.Stats[mainDraggedStat.Type], mainContainer,
                mainDraggedStat.Value, mainDraggedStat.IsPercentageValue);
                
            foreach (var stat in slot.EquipmentItem.AdditionalStats)
            {
                displayStats.Add(_displayStatFactory.Create(_displayStatData.Stats[stat.Type], additionalContainer,
                    stat.Value, stat.IsPercentageValue));
            }
        }
    }
}