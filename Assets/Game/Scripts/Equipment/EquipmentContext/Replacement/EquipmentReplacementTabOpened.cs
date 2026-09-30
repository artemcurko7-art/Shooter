using Game.Scripts.Equipment.EquipmentContext.Handler;
using Game.Scripts.Service.Subscriber;

namespace Game.Scripts.Equipment.EquipmentContext.Replacement
{
    public class EquipmentReplacementTabOpened : ISubscriber
    {
        private readonly ITabService<EquipmentSlotHandler> _tabService;
        private readonly EquipmentDisplayReplacement _displayReplacement;

        public EquipmentReplacementTabOpened(ITabService<EquipmentSlotHandler> tabService, EquipmentDisplayReplacement displayReplacement)
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