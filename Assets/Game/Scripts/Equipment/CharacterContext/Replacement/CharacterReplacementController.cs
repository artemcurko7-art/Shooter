using Game.Scripts.Equipment.CharacterContext.DragInDrop;
using Game.Scripts.Equipment.CharacterContext.Handler;
using Game.Scripts.Equipment.CharacterContext.Provider;
using Game.Scripts.Equipment.Repository;
using Game.Scripts.Service.Equipment.CharacterContext;
using Game.Scripts.SquadContext.Type;

namespace Game.Scripts.Equipment.CharacterContext.Replacement
{
    public class CharacterReplacementController : CharacterSlotProcessor, IReplaceable<CharacterReplacementController>
    {
        private readonly ITabService<CharacterSlotHandler> _tabService;
        private bool _isTabActive;
        
        public CharacterReplacementController(
            SlotRepository<CharacterSlot> repository,
            FreeSlotRegistry<SquadNumberType, CharacterSlot> freeRegistry,
            CharacterDropSlot[] dropSlots,
            CharacterProvider provider,
            CharacterSlotRewardService service,
            ITabService<CharacterSlotHandler> tabService)
            : base(repository, freeRegistry, dropSlots, provider, service)
        {
            _tabService = tabService;
        }

        public void Replace()
        {
            foreach (var dropSlot in DropSlots)
            {
                if (dropSlot.Type == SquadNumberType.First)
                {
                    FreeRegistry.EquippedSlots[dropSlot.Type].Drag.ResetSettings();
                    dropSlot.Set(DroppedSlot);
                    FreeRegistry.Register(dropSlot.Type, DroppedSlot);
                    Provider.Set(dropSlot.Type, dropSlot.Slot.Character);
                    _tabService.DisableTab();
                }
            }
        }
    }
}