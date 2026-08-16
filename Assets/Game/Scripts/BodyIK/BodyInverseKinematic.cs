using Game.Scripts.WeaponContext.Type;
using UnityEngine;
using Zenject;

namespace Game.Scripts.BodyIK
{
    public class BodyInverseKinematic : MonoBehaviour
    {
        [Header("Правая рука")]
        [SerializeField] private Transform _handR;
        [SerializeField] private Transform _hintR;
        [SerializeField] private Transform _indexR;
        [SerializeField] private Transform _middleR;
        [SerializeField] private Transform _thumbR;
    
        [Header("Левая рука")]
        [SerializeField] private Transform _handL;
        [SerializeField] private Transform _hintL;
        [SerializeField] private Transform _indexL;
        [SerializeField] private Transform _middleL;
        [SerializeField] private Transform _thumbL;
    
        [Inject]
        public void Construct(WeaponType type, BodyInverseKinematicData data)
        {
            _handR.localPosition = data.Settings[type].HandRPosition;
            _handR.rotation = Quaternion.Euler(data.Settings[type].HandRRotation);
            _hintR.localPosition = data.Settings[type].HintRPosition;
            _hintR.rotation = Quaternion.Euler(data.Settings[type].HintRRotation);
            _indexR.localPosition = data.Settings[type].IndexRPosition;
            _indexR.rotation = Quaternion.Euler(data.Settings[type].IndexRRotation);
            _middleR.localPosition = data.Settings[type].MiddleRPosition;
            _middleR.rotation = Quaternion.Euler(data.Settings[type].MiddleRRotation);
            _thumbR.localPosition = data.Settings[type].ThumbRPosition;
            _thumbR.rotation = Quaternion.Euler(data.Settings[type].ThumbRRotation);
        
            _handL.localPosition = data.Settings[type].HandLPosition;
            _handL.rotation = Quaternion.Euler(data.Settings[type].HandLRotation);
            _hintL.localPosition = data.Settings[type].HintLPosition;
            _hintL.rotation = Quaternion.Euler(data.Settings[type].HintLRotation);
            _indexL.localPosition = data.Settings[type].IndexLPosition;
            _indexL.rotation = Quaternion.Euler(data.Settings[type].IndexLRotation);
            _middleL.localPosition = data.Settings[type].MiddleLPosition;
            _middleL.rotation = Quaternion.Euler(data.Settings[type].MiddleLRotation);
            _thumbL.localPosition = data.Settings[type].ThumbLPosition;
            _thumbL.rotation = Quaternion.Euler(data.Settings[type].ThumbLRotation);
        }
    }
}