using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.Equipment.EquipmentContext.Replacement
{
    [RequireComponent(typeof(Image))]
    public class ChildrenImagesSpriteApplier : MonoBehaviour
    {
        private readonly List<Image> _circles = new();
        private Image _background;

        [Inject]
        private void Construct()
        {
            _background = GetComponent<Image>();

            for (int i = 0; i < transform.childCount; i++)
            {
                var circles = transform.GetChild(i).GetComponentsInChildren<Image>();
                _circles.AddRange(circles);
            }
        }

        public void Set(Sprite background, Sprite circle)
        {
            _background.sprite = background;
            
            foreach (var image in _circles)
                image.sprite = circle;
        }
    }
}