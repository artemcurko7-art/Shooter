using System;
using UnityEngine;
using Zenject;

namespace Game.Scripts.UI.TabContext
{
    public class TabView : MonoBehaviour
    {
        private Transform _currentHierarchy;
        private int _indexHierarchy;

        public bool IsOpened => gameObject.activeSelf;

        public event Action Opened;
        public event Action Closed;

        [Inject]
        private void Construct()
        {
            _currentHierarchy = transform.parent;
            _indexHierarchy = transform.GetSiblingIndex();
        }

        public void Clear()
        {
            Closed?.Invoke();

            transform.SetParent(_currentHierarchy, false);
            transform.SetSiblingIndex(_indexHierarchy);
            gameObject.SetActive(false);
        }

        public void Open()
        {
            if (IsOpened)
                return;

            gameObject.SetActive(true);
            Opened?.Invoke();
        }
    }
}