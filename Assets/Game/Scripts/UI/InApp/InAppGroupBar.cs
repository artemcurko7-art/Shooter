using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game.Scripts.UI.InApp
{
    public class InAppGroupBar : MonoBehaviour
    {
        private const int MaxAvailableBarsCount = 5;

        [SerializeField] private GameObject _darkFrame;
        [SerializeField] private RectTransform _barsParent;
        [SerializeField] private TMP_Text _barsCount;
        [SerializeField] private InAppBar _prefab;

        private bool _isAvailable;
        private bool _isTaken;

        private InAppData.InAppGroup _group;
        private List<InAppBar> _bars = new();

        public void InitializeBars(InAppData.InAppGroup group)
        {
            _group = group;

            for (var i = 0; i < _group.InApps.Count && i < MaxAvailableBarsCount; i++)
            {
                var bar = Instantiate(_prefab, _barsParent);
                bar.Init(_group.InApps[i], true, false);
                _bars.Add(bar);
            }

            UpdateVisual();
        }

        public void UpdateVisual()
        {
            if (_group == null) return;

            _barsCount.text = _group.InApps.Count.ToString();

            _darkFrame.SetActive(!_isAvailable);
        }
    }
}