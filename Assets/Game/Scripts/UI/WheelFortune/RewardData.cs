using System;
using System.Collections.Generic;
using Game.Scripts.UI.Items;
using UnityEngine;

namespace Game.Scripts.UI.WheelFortune
{
    [CreateAssetMenu(fileName = "RewardData", menuName = "Game/Wheel Fortune/Rewards")]
    public class RewardData : ScriptableObject
    {
        [SerializeField] private List<WheelReward> _rewards = new();

        public List<WheelReward> Rewards => _rewards;

        [Serializable]
        public class WheelReward
        {
            public Reward Reward;
        }
    }
}