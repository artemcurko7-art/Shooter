using Game.Scripts.UI.Animation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.WheelFortune
{
    public class WheelRewardDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _previewText;
        [SerializeField] private Image _previewImage;
        [SerializeField] private AppearAnimation _rewardAnimation;

        public void Clear(string emptySymbol)
        {
            if (_previewText)
                _previewText.text = emptySymbol;

            if (_previewImage)
                _previewImage.sprite = null;
        }

        public void UpdatePreview(RewardData.WheelReward wheelReward)
        {
            if (wheelReward?.Reward == null)
                return;

            if (_previewText)
                _previewText.text = wheelReward.Reward.GetLocalizedName();

            if (_previewImage)
                _previewImage.sprite = wheelReward.Reward.Icon;
        }

        public void Show(RewardData.WheelReward reward)
        {
            if (reward?.Reward == null)
                return;

            if (!_rewardAnimation || !_previewImage)
            {
                UpdatePreview(reward);
                return;
            }

            _rewardAnimation.Play(
                _previewImage,
                () => UpdatePreview(reward)
            );
        }

        public void KillAnimation()
        {
            if (_rewardAnimation)
                _rewardAnimation.KillAnimation();
        }
    }
}