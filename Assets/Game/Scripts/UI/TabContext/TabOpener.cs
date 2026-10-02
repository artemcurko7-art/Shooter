using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.UI.TabContext
{
    public class TabOpener : Tab
    {
        [SerializeField] private int _indexOffsetParent;
        
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
                view.transform.SetSiblingIndex(_canvas.transform.childCount - 3 + _indexOffsetParent);
            }
        }

        private void OnValidate()
        {
            _indexOffsetParent = Mathf.Clamp(_indexOffsetParent, 0, 1);
        }
    }
}