using Game.Scripts.Equipment.CharacterContext.DragInDrop;
using Game.Scripts.Equipment.Repository;
using Game.Scripts.Service.Equipment.Reward;
using Game.Scripts.SquadContext.Type;
using UnityEngine;

namespace Game.Scripts.Equipment.CharacterContext.Handler
{
    public class CharacterSlotHandler : SlotHandler<CharacterSlotHandler, SquadNumberType, CharacterSlot>
    {
        private readonly CharacterDropSlot[] _dropSlots;

        public CharacterSlotHandler(
            ISlotRewardService<CharacterSlot> service,
            SlotRepository<CharacterSlot> repository,
            CharacterDropSlot[] dropSlots,
            FreeSlotRegistry<SquadNumberType, CharacterSlot> freeRegistry)
            : base(service, repository, dropSlots, freeRegistry)
        {
            _dropSlots = dropSlots;
        }

        public SquadNumberType SquadNumberType { get; private set; }

        public override void Subscribe()
        {
            base.Subscribe();

            foreach (var dropSlot in _dropSlots)
                dropSlot.TypeDropped += OnTypeDropped;
        }

        public override void Unsubscribe()
        {
            base.Unsubscribe();
            
            foreach (var dropSlot in _dropSlots)
                dropSlot.TypeDropped -= OnTypeDropped;
        }

        private void OnTypeDropped(SquadNumberType type)
        {
            SquadNumberType = type;
        }
    }
}