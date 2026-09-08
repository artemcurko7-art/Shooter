using System;
using Game.Scripts.UI.Animation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Challenges
{
    public class AchieveBar : MonoBehaviour
    {
        [SerializeField] private Image _background;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _lock;
        [SerializeField] private Image _checkMark;
        [SerializeField] private Sprite _redBackgroundSprite;
        [SerializeField] private Sprite _greenBackgroudSprite;
        [SerializeField] private Sprite _redFrameSprite;
        [SerializeField] private Sprite _greenFrameSprite;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private TextAppear _textAppear;

        private bool _isOpened;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _textAppear.gameObject.SetActive(false);
            _checkMark.gameObject.SetActive(_isOpened);
            _lock.gameObject.SetActive(!_isOpened);
        }
    }
}