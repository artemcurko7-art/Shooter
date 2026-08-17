using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Equipment
{
    public class Slot : MonoBehaviour
    {
        [SerializeField] private Image _rarity;
        [SerializeField] private Image _icon;
        
        public void Initialize(Sprite rarity, Sprite icon)
        {
            _rarity.sprite = rarity;
            _icon.sprite = icon;
        }
    }
}