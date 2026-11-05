using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Avatars
{
    [RequireComponent(typeof(Button))]
    public class AvatarBar : MonoBehaviour
    {
        [Header("UI Элементы")]
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _lock;
        [SerializeField] private GameObject _selectedFrame;
        [SerializeField] private GameObject _openedFrame;
        [SerializeField] private GameObject _lockedFrame;

        private AvatarData.Avatar _avatar;
        private Avatars _avatars;

        private Button _button;

        public AvatarData.Avatar GetAvatarData() => _avatar;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        public void Init(AvatarData.Avatar avatar, Avatars avatars)
        {
            _avatar = avatar;
            _avatars = avatars;

            if (!_icon)
            {
                Debug.LogError($"[AvatarBar] Не назначен компонент Image '_icon' на объекте {gameObject.name}");
                return;
            }

            if (avatar?.Icon)
            {
                _icon.sprite = avatar.Icon;
            }
            else
            {
                _icon.sprite = null;
                Debug.LogWarning($"[AvatarBar] У аватара  нет иконки!");
            }
        }

        public void UpdateVisual(bool isOpen, bool isSelected)
        {
            if (isSelected) isOpen = true;

            SetActive(_openedFrame, isOpen && !isSelected);
            SetActive(_selectedFrame, isOpen && isSelected);
            SetActive(_lockedFrame, !isOpen);
            SetActive(_lock, !isOpen);
        }

        private void SetActive(GameObject target, bool active)
        {
            if (target) target.SetActive(active);
        }

        private void OnButtonClick()
        {
            _avatars?.SelectAvatar(_avatar.Id);
        }
    }
}