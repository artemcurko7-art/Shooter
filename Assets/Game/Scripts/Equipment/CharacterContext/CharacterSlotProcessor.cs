using Game.Scripts.Equipment.CharacterContext.DragInDrop;
using Game.Scripts.Equipment.CharacterContext.Provider;
using Game.Scripts.Equipment.Repository;
using Game.Scripts.Extensions;
using Game.Scripts.Service.Equipment.Reward;
using Game.Scripts.SquadContext.Type;
using UnityEngine;

namespace Game.Scripts.Equipment.CharacterContext
{
    public class CharacterSlotProcessor : SlotProcessor<SquadNumberType, CharacterSlot>
    {
        private readonly CharacterDropSlot[] _dropSlots;
        private readonly CharacterProvider _provider;
        private SquadNumberType _squadNumberType;

        protected CharacterSlotProcessor(
            ISlotRewardService<CharacterSlot> service,
            SlotRepository<CharacterSlot> repository,
            CharacterDropSlot[] dropSlots,
            FreeSlotRegistry<SquadNumberType, CharacterSlot> freeRegistry,
            CharacterProvider provider)
            : base(service, repository, dropSlots, freeRegistry)
        {
            _dropSlots = dropSlots;
            _provider = provider;
        }
        
        //protected SortingEquipmentByParameters Sorting { get; }

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
        
        protected override void OnBeginDragged(CharacterSlot slot)
        {
            base.OnBeginDragged(slot);
            
            foreach (var dropSlot in _dropSlots)
            {
                if (FreeRegistry.EquippedSlots[dropSlot.Type] == slot)
                {
                    FreeRegistry.Unregister(dropSlot.Type);
                    _provider.Remove(dropSlot.Type);
                }
            }
        }

        protected override void OnDropped(CharacterSlot slot)
        {
            base.OnDropped(slot);
            
            if (FreeRegistry.EquippedSlots[_squadNumberType] == null)
            {
                FreeRegistry.Register(_squadNumberType, slot);
                _provider.Set(_squadNumberType, slot.Type);
            }
        }

        private void OnTypeDropped(SquadNumberType type)
        {
            _squadNumberType = type;
        }
    }
}