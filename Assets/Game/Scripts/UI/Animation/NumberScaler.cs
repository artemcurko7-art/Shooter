using System.Globalization;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Animation
{
    public class NumberScaler : MonoBehaviour
    {
        [SerializeField] private Ease _ease = Ease.InOutExpo;
        [SerializeField] private TMP_Text _target;
        [SerializeField] private int _from;
        [SerializeField] private int _to = 999;
        [SerializeField] private float _duration = 3f;

        private void OnEnable()
        {
            Animate();
        }

        private void Awake()
        {
            _from = 0;
        }

        public void Animate()
        {
            if (_target.text == null) return;
            _target.text = _from.ToString();

            var currentValue = _from;

            DOTween.To(() => currentValue, value =>
                {
                    currentValue = value;
                    _target.text = currentValue.ToString();
                },
                _to, _duration).SetEase(_ease);
        }
    }
}