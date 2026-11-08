using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Profile.Avatars
{
    public class AvatarIconDisplayer : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private AvatarsHandler _avatarsHandler;

        private void OnEnable()
        {
            if (!_avatarsHandler) return;

            RefreshIcon();
            _avatarsHandler.Selected += UpdateVisual;
        }

        private void OnDisable()
        {
            if (!_avatarsHandler) return;

            _avatarsHandler.Selected -= UpdateVisual;
        }

        private void RefreshIcon()
        {
            if (!_image || !_avatarsHandler) return;

            var currentSprite = _avatarsHandler.GetCurrentSelectedSprite();
            
            if (currentSprite != null)
                _image.sprite = currentSprite;
        }

        private void UpdateVisual(Sprite icon)
        {
            if (!_image) return;
            _image.sprite = icon;
        }
    }
}