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
            if (Views == null || Views.Count == 0)
                return;

            var isAlreadyOpened = true;

            foreach (var view in Views)
            {
                if (!view || !view.IsOpened)
                {
                    isAlreadyOpened = false;
                    break;
                }
            }

            if (isAlreadyOpened)
                return;

            base.OnClick();

            foreach (var view in Views)
            {
                if (!view)
                    continue;

                view.transform.SetParent(_canvas.transform);
                view.transform.SetSiblingIndex(
                    _canvas.transform.childCount - 3 + _indexOffsetParent);

                view.Open();
            }
        }

        private void OnValidate()
        {
            _indexOffsetParent = Mathf.Clamp(_indexOffsetParent, 0, 1);
        }
    }
}