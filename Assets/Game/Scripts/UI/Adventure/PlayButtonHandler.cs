using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Adventure
{
    public class PlayButtonHandler : MonoBehaviour
    {
        [SerializeField] private Button _playButton;

        public event Action Clicked;

        private void OnEnable()
        {
            _playButton.onClick.AddListener(OnPlayButtonClick);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(OnPlayButtonClick);
        }

        private void OnPlayButtonClick()
        {
            Clicked?.Invoke();
        }
    }
}