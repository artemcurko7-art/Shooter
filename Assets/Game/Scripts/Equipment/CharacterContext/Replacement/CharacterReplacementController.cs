using Game.Scripts.Equipment.CharacterContext.DragInDrop;
using Game.Scripts.Equipment.CharacterContext.Handler;
using Game.Scripts.Equipment.CharacterContext.Provider;
using Game.Scripts.Equipment.Repository;
using Game.Scripts.Service.Equipment.Reward;
using Game.Scripts.SquadContext.Type;

namespace Game.Scripts.Equipment.CharacterContext.Replacement
{
    public class CharacterReplacementController : ReplacementController<CharacterReplacementController, SquadNumberType, CharacterSlot>
    {
        private readonly CharacterDropSlot[] _dropSlots;
        private readonly CharacterProvider _provider;
        private readonly ITabService<CharacterSlotHandler> _tabService;
        private bool _isTabActive;
        
        public CharacterReplacementController(
            ISlotRewardService<CharacterSlot> service,
            SlotRepository<CharacterSlot> repository,
            CharacterDropSlot[] dropSlots,
            FreeSlotRegistry<SquadNumberType, CharacterSlot> freeRegistry,
            CharacterProvider provider,
            ITabService<CharacterSlotHandler> tabService)
            : base(service, repository, dropSlots, freeRegistry)
        {
            _dropSlots = dropSlots;
            _provider = provider;
            _tabService = tabService;
        }

        public override void Replace()
        {
            foreach (var dropSlot in _dropSlots)
            {
                if (dropSlot.Type == SquadNumberType.Second)
                {
                    FreeRegistry.EquippedSlots[dropSlot.Type].Drag.ResetSettings();
                    dropSlot.Set(DroppedSlot);
                    FreeRegistry.Register(dropSlot.Type, DroppedSlot);
                    _provider.Set(dropSlot.Type, dropSlot.Slot.Character);
                    _tabService.DisableTab();
                }
            }
        }
    }
}