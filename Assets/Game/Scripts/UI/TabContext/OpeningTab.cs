using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.UI.TabContext
{
    public class OpeningTab : MonoBehaviour
    {
        [SerializeField] private TabView _view;
        [SerializeField] private Button _button;

        private Tab _tab;
        
        [Inject]
        public void Construct(Tab tab)
        {
            _tab = tab;
        }
        
        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            foreach (var view in _tab.Views)
                view.gameObject.SetActive(false);
            
            _view.gameObject.SetActive(true);
        }
    }
}