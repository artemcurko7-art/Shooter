using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;
using YG;

namespace Game.Scripts.UI.Avatars
{
    public class Avatars : MonoBehaviour
    {
        [Header("Настройки лимита")]
        [SerializeField] private bool _enableLimit = true;
        [SerializeField, MaxValue(24)] private int _maxLimit = 24;

        [Header("Зависимости")]
        [SerializeField] private AvatarData _data;
        [SerializeField] private AvatarBar _prefab;

        private readonly List<AvatarBar> _bars = new List<AvatarBar>();
        
        private HashSet<string> _openedAvatarsSet;
        private Dictionary<string, int> _avatarIndexMap;

        private void Awake()
        {
            InitializeSavesCache();
        }

        public void GenerateAvatars()
        {
            if (!_data || !_prefab)
            {
                Debug.LogWarning($"[Avatars] Не назначены зависимости: Data={_data}, Prefab={_prefab}. Проверьте Inspector.");
                return;
            }

            ClearAvatars();
            InitializeSavesCache();

            var countToGenerate = _data.Avatars.Count;
            
            if (_enableLimit && countToGenerate > _maxLimit)
            {
                countToGenerate = _maxLimit;
                Debug.Log($"[Avatars] Лимит включен. Отображаем только первые {_maxLimit} аватаров из {_data.Avatars.Count}.");
            }

            for (var i = 0; i < countToGenerate; i++)
            {
                var avatar = _data.Avatars[i];
                
                if (avatar == null) continue;

                var bar = Instantiate(_prefab, transform);
                bar.Init(avatar);
                _bars.Add(bar);
            }

            RefreshVisuals();
        }

        public void ClearAvatars()
        {
            foreach (var bar in _bars)
            {
                if (bar && bar.gameObject)
                {
                    Destroy(bar.gameObject);
                }
            }
            _bars.Clear();
        }

        public void RefreshVisuals()
        {
            InitializeSavesCache();

            foreach (var bar in _bars)
            {
                if (!bar) continue;
                
                var currentAvatar = bar.GetAvatarData(); 

                if (currentAvatar == null || string.IsNullOrEmpty(currentAvatar.Id)) continue;

                var isOpen = _openedAvatarsSet.Contains(currentAvatar.Id);
                var isSelected = isOpen && currentAvatar.Id == YG2.saves.SelectedAvatarId;

                bar.UpdateVisual(isOpen, isSelected);
            }
        }
        
        private void InitializeSavesCache()
        {
            _openedAvatarsSet = new HashSet<string>(YG2.saves.OpenedAvatarIds);
            
            _avatarIndexMap = new Dictionary<string, int>();
            for (var i = 0; i < YG2.saves.OpenedAvatarIds.Count; i++)
            {
                _avatarIndexMap[YG2.saves.OpenedAvatarIds[i]] = i;
            }
        }

        public void OnAvatarUnlocked(string newAvatarId)
        {
            if (_openedAvatarsSet.Contains(newAvatarId))
            {
                return;
            }

            YG2.saves.OpenedAvatarIds.Add(newAvatarId);
            YG2.SaveProgress(); 

            InitializeSavesCache(); 
            RefreshVisuals();
            
            if (_enableLimit && _bars.Count < _data.Avatars.Count)
            {
                GenerateAvatars(); 
            }
        }
        
        public void SelectAvatar(string avatarId)
        {
            if (!_openedAvatarsSet.Contains(avatarId))
            {
                Debug.LogWarning($"[Avatars] Попытка выбрать закрытый аватар: {avatarId}");
                return;
            }

            YG2.saves.SelectedAvatarId = avatarId;
            YG2.SaveProgress();
            RefreshVisuals();
        }
    }
}
