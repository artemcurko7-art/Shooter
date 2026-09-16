using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Game.Scripts.UI.Map
{
    public class MapBar : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Image _starPrefab;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private Transform _starsParent;
        [SerializeField] private RectTransform _bossIcon;
        [SerializeField] private Color _easyColor;
        [SerializeField] private Color _normalColor;
        [SerializeField] private Color _hardColor;

        [Header("Анимация босса")]
        [SerializeField] private float _bossMoveDistance = 500f;
        [SerializeField] private float _bossMoveDelay = 0.1f;
        [SerializeField] private float _bossMoveDuration = 0.3f;
        [SerializeField] private Ease _bossMoveEase = Ease.OutBack;

        private MapData.Map _map;

        private Vector2 _bossInitialPosition;
        private Sequence _bossSequence;

        public void Init(MapData.Map map)
        {
            _map = map;

            _icon.sprite = map.icon;
            _name.text = map.GetLocalizedName(YG2.lang);

            if (_bossIcon)
            {
                _bossInitialPosition = _bossIcon.anchoredPosition;
                _bossIcon.gameObject.SetActive(_map.hasBoss);
            }

            InitializeDifficulty();
        }

        public void SetSelected(bool selected, bool animated = true)
        {
            if (!_map.hasBoss || !_bossIcon)
                return;

            _bossSequence?.Kill();

            var targetPosition = _bossInitialPosition;

            if (selected)
                targetPosition.y += _bossMoveDistance;

            if (!animated)
            {
                _bossIcon.anchoredPosition = targetPosition;
                return;
            }

            _bossSequence = DOTween.Sequence();

            _bossSequence.AppendInterval(_bossMoveDelay);

            _bossSequence.Append(
                _bossIcon
                    .DOAnchorPos(targetPosition, _bossMoveDuration)
                    .SetEase(_bossMoveEase)
            );

            _bossSequence.OnComplete(() => _bossSequence = null);
        }

        private void InitializeDifficulty()
        {
            if (_map.difficulty == 0)
                return;

            var color = _map.difficulty switch
            {
                1 => _easyColor,
                2 => _normalColor,
                3 => _hardColor,
                _ => Color.white
            };

            for (var i = 0; i < _map.difficulty; i++)
            {
                var star = Instantiate(_starPrefab, _starsParent);
                star.color = color;
            }
        }

        private void OnDestroy()
        {
            _bossSequence?.Kill();
        }
    }
}