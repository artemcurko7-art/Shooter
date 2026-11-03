using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Avatars
{
    [RequireComponent(typeof(RectTransform))]
    public class AvatarBar : MonoBehaviour
    {
        [Header("UI Элементы")]
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _frame;   
        [SerializeField] private GameObject _lock; 
        [SerializeField] private GameObject _targetFrame;

        private AvatarData.Avatar _avatar;

        public AvatarData.Avatar GetAvatarData() => _avatar;
        
        public void Init(AvatarData.Avatar avatar)
        {
            _avatar = avatar;

            if (_icon == null)
            {
                Debug.LogError($"[AvatarBar] Не назначен компонент Image '_icon' на объекте {gameObject.name}");
                return;
            }

            if (avatar?.Icon != null)
            {
                _icon.sprite = avatar.Icon;
            }
            else
            {
                _icon.sprite = null;
                Debug.LogWarning($"[AvatarBar] У аватара '{avatar?.Name ?? "Unknown"}' нет иконки!");
            }
        }

        public void UpdateVisual(bool isOpen, bool isSelected)
        {
            if (_lock != null) _lock.SetActive(!isOpen);
            if (_frame != null) _frame.SetActive(isOpen && !isSelected);
            if (_targetFrame != null) _targetFrame.SetActive(isOpen && isSelected);
        }
    }
}
