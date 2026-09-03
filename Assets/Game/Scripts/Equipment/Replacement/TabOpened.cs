using Game.Scripts.Service.Subscriber;

namespace Game.Scripts.Equipment.Replacement
{
    public class TabOpened : ISubscriber
    {
        private readonly ITabService _tabService;
        private readonly DisplayReplacement _displayReplacement;

        public TabOpened(ITabService tabService, DisplayReplacement displayReplacement)
        {
            _tabService = tabService;
            _displayReplacement = displayReplacement;
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
            _displayReplacement.gameObject.SetActive(isActive);
        }
    }
}