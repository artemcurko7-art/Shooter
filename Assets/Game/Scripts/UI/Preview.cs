using Game.Scripts.Genetic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Game.Scripts.UI
{
    [RequireComponent(typeof(PreviewTransition))]
    public class Preview : MonoBehaviour
    {
        [SerializeField] private GeneticSystem _geneticSystem;
        [SerializeField] private ImageBlicker _imageBlicker;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private RawImage _background;
        [SerializeField] private Image _iconFrame;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _buyButtonText;
        [SerializeField] private Button _buyStatButton;
        [SerializeField] private Button _exitButton;

        private PreviewTransition _transition;
        private string _statName;
        private Color _originColor;
        private Color _activeColor;

        private void OnEnable()
        {
            _buyStatButton.onClick.AddListener(OnBuyButtonClick);
            _exitButton.onClick.AddListener(OnExitButtonClick);
        }

        private void OnDisable()
        {
            _buyStatButton.onClick.RemoveListener(OnBuyButtonClick);
            _exitButton.onClick.RemoveListener(OnExitButtonClick);
        }

        private void Awake()
        {
            _transition = GetComponent<PreviewTransition>();

            Color color = Color.black;

            color.a = 0;
            _originColor = color;

            color.a = 0.7f;
            _activeColor = color;
        }

        public void Open(StatsData.Stat stat, Vector3 startPosition)
        {
            _statName = stat.name;

            _icon.sprite = stat.icon;
            _title.text = stat.GetLocalizedName(YG2.lang);
            _buyButtonText.text = GetLocalizedBuyText();

            _background.color = _activeColor;
            _scrollRect.enabled = false;

            bool canBuy = _buyStatButton.interactable;

            if (_imageBlicker != null)
            {
                _imageBlicker.ResetToBaseColor();

                if (canBuy)
                {
                    _imageBlicker.Enable();
                }
                else
                {
                    _imageBlicker.Disable();
                }
            }

            _transition.Open(startPosition);
        }


        private void OnBuyButtonClick()
        {
            _geneticSystem.IncreaseStat(_statName);
            Close();
        }

        private void Close()
        {
            if (_imageBlicker != null)
            {
                _imageBlicker.Disable();
            }

            _background.color = _originColor;
            _scrollRect.enabled = true;
            _transition.Close();
            _statName = string.Empty;
        }


        private void OnExitButtonClick()
        {
            Close();
        }

        private string GetLocalizedBuyText()
        {
            string languageCode = YG2.lang;

            return languageCode switch
            {
                "ru" => "Получить!",
                "en" => "Receive!",
                "tr" => "almakt?r",
                _ => "Receive!",
            };
        }
    }
}