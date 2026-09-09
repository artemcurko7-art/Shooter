using Game.Scripts.PlayerContext.GameInput;
using UnityEngine;

namespace Game.Scripts.PlayerContext
{
    public class Mover
    {
        private readonly IInput _input;
        private Vector3 _currentVelocity;
        
        public Mover(IInput input)
        {
            _input = input;
        }
        
        public Vector3 Direction { get; private set; }
        
        public void Move(CharacterController characterController, float acceleration, float deceleration, float speed)
        {
            Direction = new Vector3(_input.Horizontal, 0f, _input.Vertical).normalized;
            Vector3 targetVelocity = Direction * speed;

            float rate = Direction.sqrMagnitude > 0.001f ? acceleration : deceleration;
            _currentVelocity = Vector3.MoveTowards(_currentVelocity, targetVelocity, rate * Time.deltaTime);

            characterController.Move(_currentVelocity * Time.deltaTime);
        }
    }
}