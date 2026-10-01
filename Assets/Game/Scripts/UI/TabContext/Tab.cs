using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.UI.TabContext
{
    public abstract class Tab : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private TabView[] _views;
        
        [field: SerializeField] protected List<TabView> Views { get; private set; }
        
        [Inject]
        public void Construct(TabView[] views)
        {
            _views = views;
        }
        
        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        protected virtual void OnClick()
        {
            foreach (var view in _views)
                view.Clear();
        }
    }
}