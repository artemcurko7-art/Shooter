using Game.Scripts.UI.Animation;
using Game.Scripts.UI.Items;
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

        public void UpdatePreview(Reward reward)
        {
            if (reward == null)
                return;

            if (_previewText)
                _previewText.text = reward.GetLocalizedName();

            if (_previewImage)
                _previewImage.sprite = reward.Icon;
        }

        public void Show(Reward reward)
        {
            if (reward == null)
                return;

            if (!_rewardAnimation || !_previewImage)
            {
                UpdatePreview(reward);
                return;
            }

            _rewardAnimation.Play(_previewImage, () => UpdatePreview(reward));
        }

        public void KillAnimation()
        {
            if (_rewardAnimation)
                _rewardAnimation.KillAnimation();
        }
    }
}