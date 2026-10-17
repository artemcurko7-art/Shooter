using System;
using UnityEngine;

namespace Game.Scripts.UI.Items
{
    [Serializable]
    public class Reward
    {
        [SerializeField] private RewardDefinition _definition;
        [SerializeField, Min(1)] private int _count = 1;

        public RewardDefinition Definition => _definition;
        public int Count => _count;

        public string Id => _definition ? _definition.Id : string.Empty;

        public Sprite Icon => _definition ? _definition.GetIcon(_count) : null;

        public GameObject Prefab => _definition ? _definition.Prefab : null;

        public string GetLocalizedName()
        {
            return _definition ? _definition.GetLocalizedName() : string.Empty;
        }

        public string GetLocalizedTypeName()
        {
            return _definition ? _definition.GetLocalizedTypeName() : string.Empty;
        }
    }
}