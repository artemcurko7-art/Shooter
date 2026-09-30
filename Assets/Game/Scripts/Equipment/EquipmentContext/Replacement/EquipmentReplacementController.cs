using Game.Scripts.Equipment.CharacterContext.Replacement;
using Game.Scripts.Equipment.EquipmentContext.DragInDrop;
using Game.Scripts.Equipment.EquipmentContext.Handler;
using Game.Scripts.Equipment.EquipmentContext.Type;
using Game.Scripts.Equipment.Repository;
using Game.Scripts.Service.Equipment.EquipmentContext;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Equipment.EquipmentContext.Replacement
{
    public class EquipmentReplacementController : EquipmentSlotProcessor, IReplaceable<EquipmentReplacementController>
    {
        private readonly ITabService<EquipmentSlotHandler> _tabService;
        private readonly GridLayoutGroup _gridLayoutGroup;
        private bool _isTabActive;

        public EquipmentReplacementController(
            IEquipmentService equipmentService,
            SlotRepository<EquipmentSlot> repository,
            FreeSlotRegistry<EquipmentType, EquipmentSlot> freeRegistry,
            EquipmentDropSlot[] dropSlots,
            SortingEquipmentByParameters sorting,
            ITabService<EquipmentSlotHandler> tabService,
            GridLayoutGroup gridLayoutGroup)
            : base(equipmentService, repository, freeRegistry, dropSlots, sorting)
        {
            _tabService = tabService;
            _gridLayoutGroup = gridLayoutGroup;
        }

        public override void Subscribe()
        {
            base.Subscribe();
            
            _tabService.TabOpened += OnTabOpened;
        }

        public new void Unsubscribe()
        {
            base.Unsubscribe();
            
            _tabService.TabOpened -= OnTabOpened;
        }

        public void Replace()
        {
            foreach (var dropSlot in DropSlots)
            {
                if (dropSlot.EquipmentType == DroppedSlot.EquipmentItem.Type)
                {
                    FreeRegistry.EquippedSlots[dropSlot.EquipmentType].Drag.ResetSettings();
                    dropSlot.Set(DroppedSlot);
                    FreeRegistry.Register(dropSlot.EquipmentType, DroppedSlot);
                    _tabService.DisableTab();
                    _gridLayoutGroup.enabled = true;
                }
            }
        }
        
        protected override void OnEndDragged(EquipmentSlot slot)
        {
            if (_isTabActive == false)
                _gridLayoutGroup.enabled = true;
        }

        private void OnTabOpened(bool isActive)
        {
            _isTabActive = isActive;
        }
    }
}