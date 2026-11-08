using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.UI.Avatars;
using NaughtyAttributes;
using UnityEngine;
using YG;

namespace Game.Scripts.UI.Profile.Avatars
{
    public class AvatarsHandler : MonoBehaviour
    {
        private const string FirstAvatarId = "1";
        private readonly List<AvatarBar> _bars = new();

        [Header("Настройки лимита")]
        [SerializeField] private bool _enableLimit = true;
        [SerializeField, MaxValue(24)] private int _maxLimit = 24;

        [Header("Зависимости")]
        [SerializeField] private AvatarData _data;
        [SerializeField] private AvatarBar _prefab;
        [SerializeField] private RectTransform _container;

        public event Action<Sprite> Selected;

        public void Setup()
        {
            InitializeAvatars();
            LoadCurrentAvatar();
        }

        public Sprite GetCurrentSelectedSprite()
        {
            var savedId = YG2.saves.SelectedAvatarId;

            if (string.IsNullOrEmpty(savedId)) return GetFallbackAvatar()?.Icon;
            
            var selected = _data.Avatars.FirstOrDefault(a => a != null && a.Id == savedId);
            return selected?.Icon ? selected.Icon : GetFallbackAvatar()?.Icon;
        }

        private AvatarData.Avatar GetFallbackAvatar()
        {
            if (!_data || _data.Avatars == null || _data.Avatars.Count == 0)
                return null;

            var defaultAvatar = _data.Avatars.FirstOrDefault(a => a != null && a.Id == FirstAvatarId);
            return defaultAvatar ?? _data.Avatars.FirstOrDefault(a => a != null && !string.IsNullOrEmpty(a.Id));
        }

        private void LoadCurrentAvatar()
        {
            var openedIds = YG2.saves.OpenedAvatarIds;

            if (openedIds.Count == 0 && HasAvatar(FirstAvatarId))
                openedIds.Add(FirstAvatarId);

            var savedId = YG2.saves.SelectedAvatarId;

            if (!string.IsNullOrEmpty(savedId) && HasAvatar(savedId))
            {
                SelectAvatar(savedId);
                return;
            }

            var fallbackId = GetFallbackAvatar()?.Id;

            if (string.IsNullOrEmpty(fallbackId))
            {
                Debug.LogWarning("[AvatarsHandler] В AvatarData нет ни одной валидной аватарки — выбрать нечего.");
                return;
            }

            YG2.saves.SelectedAvatarId = fallbackId;
            YG2.SaveProgress();
            SelectAvatar(fallbackId);
        }

        private bool HasAvatar(string avatarId)
        {
            return _data && _data.Avatars.Any(a => a != null && a.Id == avatarId);
        }

        private void InitializeAvatars()
        {
            if (!_data || !_prefab)
            {
                Debug.LogWarning(
                    $"[AvatarsHandler] Не назначены зависимости: Data={_data}, Prefab={_prefab}. Проверьте Inspector.");
                return;
            }

            ClearAvatars();

            var countToGenerate = _data.Avatars.Count;

            if (_enableLimit && countToGenerate > _maxLimit)
            {
                countToGenerate = _maxLimit;
                Debug.Log(
                    $"[AvatarsHandler] Лимит включен. Отображаем только первые {_maxLimit} аватаров из {_data.Avatars.Count}.");
            }

            for (var i = 0; i < countToGenerate; i++)
            {
                var avatar = _data.Avatars[i];

                if (avatar == null) continue;

                var bar = Instantiate(_prefab, _container);
                bar.Init(avatar, this);
                _bars.Add(bar);
            }

            RefreshVisuals();
        }

        private void ClearAvatars()
        {
            foreach (var bar in _bars.Where(bar => bar && bar.gameObject))
            {
                Destroy(bar.gameObject);
            }

            _bars.Clear();
        }

        private void RefreshVisuals()
        {
            foreach (var bar in _bars)
            {
                if (!bar) continue;

                var currentAvatar = bar.GetAvatarData();

                if (currentAvatar == null || string.IsNullOrEmpty(currentAvatar.Id)) continue;

                var isOpen = YG2.saves.OpenedAvatarIds.Contains(currentAvatar.Id);
                var isSelected = isOpen && currentAvatar.Id == YG2.saves.SelectedAvatarId;

                bar.UpdateVisual(isOpen, isSelected);
            }
        }

        public void Unlock(string newAvatarId)
        {
            if (YG2.saves.OpenedAvatarIds.Contains(newAvatarId))
            {
                return;
            }

            YG2.saves.OpenedAvatarIds.Add(newAvatarId);
            YG2.SaveProgress();

            RefreshVisuals();
        }

        public void SelectAvatar(string avatarId)
        {
            if (!YG2.saves.OpenedAvatarIds.Contains(avatarId))
            {
                Debug.LogWarning($"[AvatarsHandler] Попытка выбрать закрытый аватар: {avatarId}");
                return;
            }

            YG2.saves.SelectedAvatarId = avatarId;
            YG2.SaveProgress();
            RefreshVisuals();

            var bar = _bars.FirstOrDefault(bar => bar && bar.GetAvatarData().Id == avatarId);
            if (!bar) return;

            Selected?.Invoke(bar.GetAvatarData().Icon);
        }
    }
}
