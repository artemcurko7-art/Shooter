using Game.Scripts.Animation;
using Game.Scripts.PlayerContext.GameInput;
using UnityEngine;
using Zenject;

namespace Game.Scripts.PlayerContext
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _deceleration;
        [SerializeField] private float _smooth;
        
        private Mover _mover;
        private IInput _input;
        private CharacterController _characterController;
        
        [Inject]
        public void Construct(Mover mover, IInput input)
        {
            _mover = mover;
            _input = input;
            
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            _input.Update();
            _mover.Move(_characterController, _acceleration, _deceleration, _speed);
        }
    }
}