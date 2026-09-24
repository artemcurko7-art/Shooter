using System;
using System.Collections.Generic;
using Game.Scripts.UI.Animation;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Shop
{
    public class Categories : MonoBehaviour
    {
        [SerializeField] List<CategoryButton> _buttons;
        [SerializeField] private SmoothScroll _scroll;

        private void Start()
        {
            Refresh();
        }

        private void Refresh()
        {
            foreach (var button in _buttons)
            {
                button.UpdateVisual();
            }
        }

        public void GetTarget(RectTransform target)
        {
            _scroll.ScrollToY(target.position.y, 1f);
        }
    }
}