using Game.Scripts.Equipment.DragInDrop;
using Game.Scripts.Service.Subscriber;

namespace Game.Scripts.Equipment.Replacement
{
    public class TabOpened : ISubscriber
    {
        private readonly ITabService _tabService;
        private readonly TabReplacement[] _replacements;

        public TabOpened(ITabService tabService, TabReplacement[] replacements)
        {
            _tabService = tabService;
            _replacements = replacements;
        }

        public void Subscribe()
        {
            _tabService.TabOpened += OnTabOpened;
        }

        public void Unsubscribe()
        {
            _tabService.TabOpened -= OnTabOpened;
        }

        private void OnTabOpened(bool isActive)
        {
            foreach (var exchange in _replacements)
                exchange.gameObject.SetActive(isActive);
        }
    }
}