using Game.Scripts.UI.TabContext;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Equipment
{
    public class TabStats : MonoBehaviour
    {
        [SerializeField] private TabView _tabView;
        [SerializeField] private Button _openButton;
        [SerializeField] private Button _closeButton;

        private void OnEnable()
        {
            _openButton.onClick.AddListener(OpenClick);
            _closeButton.onClick.AddListener(CloseClick);
        }

        private void OnDisable()
        {
            _openButton.onClick.RemoveListener(OpenClick);
            _closeButton.onClick.RemoveListener(CloseClick);
        }

        private void OpenClick()
        {
            _tabView.gameObject.SetActive(true);
        }
        
        private void CloseClick()
        {
            _tabView.gameObject.SetActive(false);
        }
    }
}