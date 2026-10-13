using System;
using System.Collections.Generic;
using Game.Scripts.UI.Items;
using UnityEngine;

namespace Game.Scripts.UI.InApp
{
    [CreateAssetMenu(fileName = "InAppData", menuName = "Game/In App/Data")]
    public class InAppData : ScriptableObject
    {
        [SerializeField] private List<InAppGroup> _groups = new();

        public List<InAppGroup> Groups => _groups;

        [Serializable]
        public class InAppGroup
        {
            public List<InApp> InApps = new();
        }

        [Serializable]
        public class InApp
        {
            public Sprite Frame;
            public List<Reward> Rewards = new();
        }
    }
}