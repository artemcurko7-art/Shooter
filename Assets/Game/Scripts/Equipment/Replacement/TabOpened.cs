using Game.Scripts.Service.Subscriber;

namespace Game.Scripts.Equipment.Replacement
{
    public class TabOpened : ISubscriber
    {
        private readonly ITabService _tabService;
        private readonly DisplayReplacement[] _replacements;

        public TabOpened(ITabService tabService, DisplayReplacement[] replacements)
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
            foreach (var replacement in _replacements)
                replacement.gameObject.SetActive(isActive);
        }
    }
}