using Game.Scripts.Equipment.EquipmentContext.DragInDrop;
using Game.Scripts.Equipment.EquipmentContext.Handler;
using Game.Scripts.Equipment.EquipmentContext.Type;
using Game.Scripts.Equipment.Repository;
using Game.Scripts.Service.Equipment.Reward;
using UnityEngine.UI;

namespace Game.Scripts.Equipment.EquipmentContext.Replacement
{
    public class EquipmentReplacementController : ReplacementController<EquipmentReplacementController, EquipmentType, EquipmentSlot>
    {
        private readonly EquipmentDropSlot[] _dropSlots;
        private readonly ITabService<EquipmentSlotHandler> _tabService;
        private readonly GridLayoutGroup _gridLayoutGroup;
        private bool _isTabActive;

        public EquipmentReplacementController(
            ISlotRewardService<EquipmentSlot> service,
            SlotRepository<EquipmentSlot> repository,
            EquipmentDropSlot[] dropSlots,
            FreeSlotRegistry<EquipmentType, EquipmentSlot> freeRegistry,
            ITabService<EquipmentSlotHandler> tabService,
            GridLayoutGroup gridLayoutGroup)
            : base(service, repository, dropSlots, freeRegistry)
        {
            _dropSlots = dropSlots;
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

        public override void Replace()
        {
            foreach (var dropSlot in _dropSlots)
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
            base.OnEndDragged(slot);
            
            if (_isTabActive == false)
                _gridLayoutGroup.enabled = true;
        }

        private void OnTabOpened(bool isActive)
        {
            _isTabActive = isActive;
        }
    }
}