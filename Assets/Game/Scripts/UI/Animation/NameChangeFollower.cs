using TMPro;
using UnityEngine;

namespace Game.Scripts.UI.Animation
{
    public class NameChangeFollower : MonoBehaviour
    {
        [SerializeField] private IconScalerGroup _group;
        [SerializeField] private TMP_Text _name;

        private void OnEnable()
        {
            _group.NameChanged += Display;
        }

        private void OnDisable()
        {
            _group.NameChanged -= Display;
        }

        private void Display(string word)
        {
            _name.text = word;
        }
    }
}