using Game.Scripts.WeaponContext.Type;
using UnityEngine;

namespace Game.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Source/Config/BodyIK", fileName = "BodyIK", order = 4)]
    public class BodyInverseKinematicConfig : ScriptableObject
    {
        [field: Header("Тип")]
        [field: SerializeField] public WeaponType Type { get; private set; }
    
        [field: Header("Позиция правой руки")]
        [field: SerializeField] public Vector3 HandRPosition { get; private set; }
        [field: SerializeField] public Vector3 HintRPosition { get; private set; }
        [field: SerializeField] public Vector3 IndexRPosition { get; private set; }
        [field: SerializeField] public Vector3 MiddleRPosition { get; private set; }
        [field: SerializeField] public Vector3 ThumbRPosition { get; private set; }
    
        [field: Header("Поворот правой руки")]
        [field: SerializeField] public Vector3 HandRRotation { get; private set; }
        [field: SerializeField] public Vector3 HintRRotation { get; private set; }
        [field: SerializeField] public Vector3 IndexRRotation { get; private set; }
        [field: SerializeField] public Vector3 MiddleRRotation { get; private set; }
        [field: SerializeField] public Vector3 ThumbRRotation { get; private set; }
    
        [field: Header("Позиция левой руки")]
        [field: SerializeField] public Vector3 HandLPosition { get; private set; }
        [field: SerializeField] public Vector3 HintLPosition { get; private set; }
        [field: SerializeField] public Vector3 IndexLPosition { get; private set; }
        [field: SerializeField] public Vector3 MiddleLPosition { get; private set; }
        [field: SerializeField] public Vector3 ThumbLPosition { get; private set; }
    
        [field: Header("Поворот левой руки")]
        [field: SerializeField] public Vector3 HandLRotation { get; private set; }
        [field: SerializeField] public Vector3 HintLRotation { get; private set; }
        [field: SerializeField] public Vector3 IndexLRotation { get; private set; }
        [field: SerializeField] public Vector3 MiddleLRotation { get; private set; }
        [field: SerializeField] public Vector3 ThumbLRotation { get; private set; }
    }
}