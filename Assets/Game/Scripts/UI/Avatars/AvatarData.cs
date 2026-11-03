using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.UI.Avatars
{
    [CreateAssetMenu(fileName = "AvatarData", menuName = "Data/AvatarData")]
    public class AvatarData : ScriptableObject
    {
        [SerializeField] private List<Avatar> _avatars = new List<Avatar>();

        public IReadOnlyList<Avatar> Avatars => _avatars;

        [Serializable]
        public class Avatar
        {
            [Tooltip("Уникальный ID аватара. Должен совпадать с тем, что хранится в YG2.saves.OpenedAvatars")]
            public string Id;

            [Tooltip("Название для отладки и UI")]
            public string Name;

            [Tooltip("Иконка, которая отобразится в списке")]
            public Sprite Icon;

        }
    }
}