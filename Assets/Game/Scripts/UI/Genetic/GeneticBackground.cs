using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Genetic
{
    public class GeneticBackground : MonoBehaviour
    {
        [SerializeField] private float _uvSpeed = 2000f;
        [SerializeField] private RawImage _background;
        [SerializeField] private RectTransform _content;

        private void LateUpdate()
        {
            if (!_background || !_content || _uvSpeed <= 0f)
                return;

            var rect = _background.uvRect;
            rect.y = _content.anchoredPosition.y / _uvSpeed;
            _background.uvRect = rect;
        }
    }
}