using Game.Scripts.Equipment.CharacterContext.DragInDrop;
using Game.Scripts.Equipment.CharacterContext.Handler;
using Game.Scripts.Equipment.CharacterContext.Repository;
using Game.Scripts.Equipment.CharacterContext.Type;
using Game.Scripts.Equipment.DragInDrop;
using Game.Scripts.Equipment.EquipmentContext;
using Game.Scripts.Equipment.Repository;
using Game.Scripts.Service.Equipment.CharacterContext;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Equipment.CharacterContext.Replacement
{
    public class CharacterReplacementController : CharacterSlotProcessor, IReplaceable<CharacterReplacementController>
    {
        private readonly ITabService<CharacterSlotHandler> _tabService;
        private readonly GridLayoutGroup _gridLayoutGroup;
        private bool _isTabActive;
        
        public CharacterReplacementController(
            SlotRepository<CharacterSlot> repository,
            FreeSlotRegistry<CharacterPlaceDropSlotType, CharacterSlot> freeRegistry,
            CharacterDropSlot[] dropSlots,
            CharacterSlotRewardService service,
            ITabService<CharacterSlotHandler> tabService,
            GridLayoutGroup gridLayoutGroup)
            : base(repository, freeRegistry, dropSlots, service)
        {
            _tabService = tabService;
            _gridLayoutGroup = gridLayoutGroup;
        }

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

        public void Replace()
        {
            foreach (var dropSlot in DropSlots)
            {
                if (dropSlot.Type == CharacterPlaceDropSlotType.First)
                {
                    FreeRegistry.EquippedSlots[dropSlot.Type].Drag.ResetSettings();
                    dropSlot.Set(DroppedSlot);
                    FreeRegistry.Register(dropSlot.Type, DroppedSlot);
                    _tabService.DisableTab();
                }
                
                //_gridLayoutGroup.enabled = true;
            }
        }
        
        protected override void OnEndDragged(CharacterSlot slot)
        {
            // if (_isTabActive == false)
            //     _gridLayoutGroup.enabled = true;
        }

        private void OnTabOpened(bool isActive)
        {
            //_isTabActive = isActive;
        }
    }
}