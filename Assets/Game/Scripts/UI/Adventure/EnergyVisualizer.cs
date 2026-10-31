using System;
using Game.Scripts.UI.Animation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Adventure
{
    public class EnergyVisualizer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _energyCount;
        [SerializeField] private PopEffect _popEffect;
        [SerializeField] private Image _energyImage;

        private int _decreaseCount = 1;
        public bool IsAnimating => _popEffect.IsAnimating;

        public void UpdateVisual(int count)
        {
            _energyCount.text = count.ToString();
        }

        public void DisplayPopEffect(Vector3 buttonPosition)
        {
            _popEffect.Animate(_energyImage.sprite, _decreaseCount, false, buttonPosition);
        }
    }
}