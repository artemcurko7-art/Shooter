using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.UI.TabContext
{
    public class TabOpener : Tab
    {
        private Canvas _canvas;

        private void Awake()
        {
            _canvas = GetComponentInParent<Canvas>();
        }

        protected override void OnClick()
        {
            base.OnClick();

            foreach (var view in Views)
            {
                view.gameObject.SetActive(true);
                view.transform.SetParent(_canvas.transform);
                view.transform.SetAsLastSibling();
            }
        }
    }
}