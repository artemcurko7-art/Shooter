using Game.Scripts.Equipment.DragInDrop;
using Game.Scripts.Equipment.Repository;
using Game.Scripts.Service.Equipment;
using UnityEngine.UI;

namespace Game.Scripts.Equipment.Replacement
{
    public class ReplacementController : SlotProcessor
    {
        private readonly ITabService _tabService;
        private readonly GridLayoutGroup _gridLayoutGroup;
        private bool _isTabActive;
        
        public ReplacementController(
            IEquipmentService equipmentService,
            EquipmentSlotRepository repository,
            EquipmentFreeSlotRegistry freeRegistry,
            SortingEquipmentByParameters sorting,
            DropSlot[] dropSlots,
            ITabService tabService,
            GridLayoutGroup gridLayoutGroup) :
            base(equipmentService, repository, freeRegistry, sorting, dropSlots)
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
        
        protected override void OnEndDragged(Slot slot)
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