using Game.Scripts.Equipment.CharacterContext.Handler;
using Game.Scripts.Equipment.EquipmentContext;
using Game.Scripts.Service.Subscriber;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Equipment.CharacterContext.Replacement
{
    public class CharacterReplacementTabOpened : ISubscriber
    {
        private readonly ITabService<CharacterSlotHandler> _tabService;
        private readonly DisplayCharacterReplacement _displayReplacement;
        private readonly Canvas _canvas;
        private readonly GridLayoutGroup _gridLayoutGroup;
        private readonly Transform _currentTransform;
        private int _indexHierarchy;
        private bool _isRepeated;

        public CharacterReplacementTabOpened(ITabService<CharacterSlotHandler> tabService, DisplayCharacterReplacement displayReplacement, Canvas canvas, GridLayoutGroup gridLayoutGroup)
        {
            _tabService = tabService;
            _displayReplacement = displayReplacement;
            _canvas = canvas;
            _gridLayoutGroup = gridLayoutGroup;
            _currentTransform = displayReplacement.RectTransform;
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
            if (isActive && _isRepeated == false)
            {
                _indexHierarchy = _displayReplacement.RectTransform.GetSiblingIndex();
                _displayReplacement.RectTransform.SetParent(_canvas.transform);
                _displayReplacement.RectTransform.SetAsLastSibling();
                _isRepeated = true;
            }
            else
            {
                _displayReplacement.RectTransform.SetParent(_gridLayoutGroup.transform);
                _displayReplacement.RectTransform.SetSiblingIndex(_indexHierarchy);
                _isRepeated = false;
            }
            
            _displayReplacement.gameObject.SetActive(isActive);
        }
    }
}