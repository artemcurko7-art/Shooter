using Game.Scripts.Equipment.DragInDrop;
using Game.Scripts.Service.Subscriber;

namespace Game.Scripts.Equipment.Replacement
{
    public class TabOpened : ISubscriber
    {
        private readonly IDropSlot[] _dropSlots;
        private readonly TabReplacement[] _exchanges;

        public TabOpened(IDropSlot[] dropSlots, TabReplacement[] exchanges)
        {
            _dropSlots = dropSlots;
            _exchanges = exchanges;
        }

        public void Subscribe()
        {
            foreach (var dropSlot in _dropSlots)
                dropSlot.TabOpened += OnTabOpened;
        }

        public void Unsubscribe()
        {
            foreach (var dropSlot in _dropSlots)
                dropSlot.TabOpened -= OnTabOpened;
        }

        private void OnTabOpened()
        {
            foreach (var exchange in _exchanges)
                exchange.gameObject.SetActive(true);
        }
    }
}