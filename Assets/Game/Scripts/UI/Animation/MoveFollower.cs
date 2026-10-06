using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Game.Scripts.UI.Animation
{
    public class MoveFollower : MonoBehaviour
    {
        [SerializeField] private IconScalerGroup _group;

        [Header("Настройки")]
        [SerializeField] private float _duration;
        [SerializeField] private Ease _ease;

        private void OnEnable()
        {
            _group.IconSelected += GoTo;
        }

        private void OnDisable()
        {
            _group.IconSelected -= GoTo;
        }

        private void GoTo(Vector3 targetPosition)
        {
            transform.DOMove(targetPosition, _duration).SetEase(_ease);
        }
    }
}