using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Equipment
{
    public class RarityEquipmentView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        
        public void Initialize(Sprite icon)
        {
            _icon.sprite = icon;
        }
    }
}