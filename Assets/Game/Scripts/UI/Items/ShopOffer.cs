using UnityEngine;

namespace Game.Scripts.UI.Items
{
    [CreateAssetMenu(fileName = "ShopOffer", menuName = "Game/Shop/Offer")]
    public class ShopOffer : ScriptableObject
    {
        [SerializeField] private ItemStack _reward;
        [SerializeField] private ItemStack _price;

        public ItemStack Reward => _reward;
        public ItemStack Price => _price;
    }
}