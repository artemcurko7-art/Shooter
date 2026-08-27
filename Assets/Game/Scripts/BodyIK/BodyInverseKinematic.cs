using Game.Scripts.Configs;
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
        public void Construct(WeaponConfig config, BodyInverseKinematicData data)
        {
            _handR.localPosition = data.Settings[config.Type].HandRPosition;
            _handR.rotation = Quaternion.Euler(data.Settings[config.Type].HandRRotation);
            _hintR.localPosition = data.Settings[config.Type].HintRPosition;
            _hintR.rotation = Quaternion.Euler(data.Settings[config.Type].HintRRotation);
            _indexR.localPosition = data.Settings[config.Type].IndexRPosition;
            _indexR.rotation = Quaternion.Euler(data.Settings[config.Type].IndexRRotation);
            _middleR.localPosition = data.Settings[config.Type].MiddleRPosition;
            _middleR.rotation = Quaternion.Euler(data.Settings[config.Type].MiddleRRotation);
            _thumbR.localPosition = data.Settings[config.Type].ThumbRPosition;
            _thumbR.rotation = Quaternion.Euler(data.Settings[config.Type].ThumbRRotation);
        
            _handL.localPosition = data.Settings[config.Type].HandLPosition;
            _handL.rotation = Quaternion.Euler(data.Settings[config.Type].HandLRotation);
            _hintL.localPosition = data.Settings[config.Type].HintLPosition;
            _hintL.rotation = Quaternion.Euler(data.Settings[config.Type].HintLRotation);
            _indexL.localPosition = data.Settings[config.Type].IndexLPosition;
            _indexL.rotation = Quaternion.Euler(data.Settings[config.Type].IndexLRotation);
            _middleL.localPosition = data.Settings[config.Type].MiddleLPosition;
            _middleL.rotation = Quaternion.Euler(data.Settings[config.Type].MiddleLRotation);
            _thumbL.localPosition = data.Settings[config.Type].ThumbLPosition;
            _thumbL.rotation = Quaternion.Euler(data.Settings[config.Type].ThumbLRotation);
        }
    }
}