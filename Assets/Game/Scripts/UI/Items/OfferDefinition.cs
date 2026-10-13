using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.UI.Items
{
    [CreateAssetMenu(fileName = "Offer", menuName = "Game/Items/Offer")]
    public class OfferDefinition : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private LocalizedName _nameTranslations;
        [SerializeField, Min(0)] private float _price;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private List<Reward> _rewards = new();

        public string Id => _id;
        public float Price => _price;
        public GameObject Prefab => _prefab;
        public IReadOnlyList<Reward> Rewards => _rewards;
    }
}