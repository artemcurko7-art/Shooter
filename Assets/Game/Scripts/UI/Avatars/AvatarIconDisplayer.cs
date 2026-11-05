using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Avatars
{
    public class AvatarIconDisplayer : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Avatars _avatars;

        private void OnEnable()
        {
            _avatars.Selected += UpdateVisual;
        }

        private void OnDisable()
        {
            _avatars.Selected -= UpdateVisual;
        }

        private void UpdateVisual(Sprite icon)
        {
            _image.sprite = icon;
        }
    }
}