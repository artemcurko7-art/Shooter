using UnityEngine;

namespace Game.Scripts.UI.DailyGift
{
    public class GiftBox : MonoBehaviour
    {
        [SerializeField] private RectTransform _rays;

        public void SwitchRay(bool isActive)
        {
            _rays.gameObject.SetActive(isActive);
        }
    }
}