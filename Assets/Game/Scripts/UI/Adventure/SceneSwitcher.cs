using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts.UI.Adventure
{
    public class SceneSwitcher : MonoBehaviour
    {
        [SerializeField] private string _name;

        public bool IsEnabled { get; private set; }

        public void LoadSavedScene()
        {
            IsEnabled = true;
            SceneManager.LoadScene(_name);
        }
    }
}