using Game.Scripts.Equipment.Type;
using UnityEngine;

namespace Game.Scripts.Equipment.Replacement
{
    public class ReplacementContainer : MonoBehaviour
    {
        [SerializeField] private Transform _mainContainerFrom;
        [SerializeField] private Transform _additionalContainerFrom;
        [SerializeField] private Transform _mainContainerTo;
        [SerializeField] private Transform _additionalContainerTo;
        
        public Transform MainContainer { get; private set; }
        public Transform AdditionalContainer { get; private set; }

        public void Select(ReplacementType type)
        {
            switch (type)
            {
                case ReplacementType.From:
                    Set(_mainContainerFrom, _additionalContainerFrom);
                    break;
                
                case ReplacementType.To:
                    Set(_mainContainerTo, _additionalContainerTo);
                    break;
            }
        }

        private void Set(Transform mainContainer, Transform additionalContainer)
        {
            MainContainer = mainContainer;
            AdditionalContainer = additionalContainer;
        }
    }
}