
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.UI.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.WheelFortune
{
    public class WheelStrip : MonoBehaviour
    {
        [SerializeField] private float _duration = 0.5f;
        [SerializeField] private float _scale = 1.4f;

        private readonly List<RewardBar> _bars = new();
        private readonly List<Reward> _rewards = new();
        private readonly List<Reward> _strip = new();

        private RectTransform _content;
        private RewardBar _barPrefab;
        private RectTransform _viewport;
        private int _bufferSize;
        private float _centerContentX;

        public IReadOnlyList<RewardBar> Bars => _bars;
        public IReadOnlyList<Reward> Rewards => _rewards;

        public float ItemWidth { get; private set; }
        public float CircleLength { get; private set; }
        public float AbsoluteScroll { get; private set; }

        public bool Initialize(
            RectTransform content,
            RewardBar barPrefab,
            BoxesData.Box box,
            RectTransform viewport,
            int bufferSize)
        {
            _content = content;
            _barPrefab = barPrefab;
            _viewport = viewport;
            _bufferSize = Mathf.Max(0, bufferSize);

            if (!_content || !_barPrefab || box == null || !_viewport || box.loots == null)
                return false;

            _rewards.Clear();

            foreach (var loot in box.loots)
            {
                if (loot != null && loot.reward != null)
                    _rewards.Add(loot.reward);
            }

            if (_rewards.Count == 0)
                return false;

            ShuffleRewards();
            BuildStrip();
            SpawnBars();

            Canvas.ForceUpdateCanvases();
            LayoutRebuilder.ForceRebuildLayoutImmediate(_content);
            Canvas.ForceUpdateCanvases();

            if (!UpdateItemWidth())
                return false;

            CircleLength = _rewards.Count * ItemWidth;

            if (CircleLength <= 0f)
                return false;

            SetInitialPosition();
            return true;
        }

        private void ShuffleRewards()
        {
            for (var i = _rewards.Count - 1; i > 0; i--)
            {
                var randomIndex = Random.Range(0, i + 1);
                (_rewards[i], _rewards[randomIndex]) = (_rewards[randomIndex], _rewards[i]);
            }
        }

        private void BuildStrip()
        {
            _strip.Clear();

            var count = _rewards.Count;
            var buffer = Mathf.Min(_bufferSize, count);

            for (var i = count - buffer; i < count; i++)
                _strip.Add(_rewards[Mod(i, count)]);

            for (var i = 0; i < count; i++)
                _strip.Add(_rewards[i]);

            for (var i = 0; i < buffer; i++)
                _strip.Add(_rewards[i % count]);
        }

        private void SpawnBars()
        {
            foreach (Transform child in _content)
            {
                if (child)
                    Destroy(child.gameObject);
            }

            _bars.Clear();

            foreach (var reward in _strip)
            {
                if (reward == null)
                    continue;

                var bar = Instantiate(_barPrefab, _content);
                bar.Init(reward);
                _bars.Add(bar);
            }
        }

        private bool UpdateItemWidth()
        {
            if (_bars.Count == 0)
                return false;

            var rect = _bars[0].RectTransform;

            if (!rect)
                return false;

            ItemWidth = rect.rect.width;
            return ItemWidth > 0f;
        }

        private void SetInitialPosition()
        {
            var buffer = Mathf.Min(_bufferSize, _rewards.Count);
            var centerIndex = Mathf.Min(buffer, _bars.Count - 1);
            var bar = _bars[centerIndex].RectTransform;

            if (!bar)
                return;

            var corners = new Vector3[4];

            _viewport.GetWorldCorners(corners);
            var viewportCenter = (corners[0] + corners[2]) * 0.5f;

            bar.GetWorldCorners(corners);
            var barCenter = (corners[0] + corners[2]) * 0.5f;

            var position = _content.position;
            position.x += viewportCenter.x - barCenter.x;
            _content.position = position;

            _centerContentX = _content.anchoredPosition.x;
            AbsoluteScroll = 0f;
            ApplyScroll();
        }

        public void SetScroll(float value)
        {
            AbsoluteScroll = value;
            ApplyScroll();
        }

        public int GetRewardIndex(bool roundToNearest = false)
        {
            if (ItemWidth <= 0f || _rewards.Count == 0)
                return 0;

            var steps = roundToNearest
                ? Mathf.RoundToInt(AbsoluteScroll / ItemWidth)
                : Mathf.FloorToInt(AbsoluteScroll / ItemWidth);

            return Mod(steps, _rewards.Count);
        }

        public Reward GetReward(int index)
        {
            if (index < 0 || index >= _rewards.Count)
                return null;

            return _rewards[index];
        }

        public void AnimateWinningBar(int rewardIndex)
        {
            if (_bars.Count == 0 || _rewards.Count == 0)
                return;

            var buffer = Mathf.Min(_bufferSize, _rewards.Count);
            var barIndex = buffer + rewardIndex;

            if (barIndex < 0 || barIndex >= _bars.Count)
                return;

            _bars[barIndex].AnimateIcon(_scale, _duration);
        }

        private void ApplyScroll()
        {
            if (CircleLength <= 0f || !_content)
                return;

            var visualScroll = Mathf.Repeat(AbsoluteScroll, CircleLength);

            _content.anchoredPosition = new Vector2(
                _centerContentX - visualScroll,
                _content.anchoredPosition.y);
        }

        private static int Mod(int value, int modulo)
        {
            if (modulo <= 0)
                return 0;

            var result = value % modulo;
            return result < 0 ? result + modulo : result;
        }
    }
}
