using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.DailyGift
{
    public class GiftCollector : MonoBehaviour
    {
        [SerializeField] private DailyGiftSystem _dailyGiftSystem;
        [SerializeField] private Button _collectButton;
        [SerializeField] private Button _doubleCollectButton;

        private void OnEnable()
        {
            _collectButton.onClick.AddListener(OnCollectButtonClick);
            _doubleCollectButton.onClick.AddListener(OnDoubleCollectButtonClick);
        }

        private void OnDisable()
        {
            _collectButton.onClick.RemoveListener(OnCollectButtonClick);
            _doubleCollectButton.onClick.RemoveListener(OnDoubleCollectButtonClick);
        }

        private void OnCollectButtonClick()
        {
            _dailyGiftSystem.Collect();
        }

        private void OnDoubleCollectButtonClick()
        {
            //реклама и тд.

            OnCollectButtonClick();
        }
    }
}