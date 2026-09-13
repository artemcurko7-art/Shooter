using UnityEngine;

namespace Game.Scripts.PhysicalBody.UnitContext.FSM.Follower
{
    public abstract class UnitStateFollower : StateMachine
    {
        private readonly CharacterController _characterController;
        private readonly Transform _current;
        private readonly Transform _target;
        private readonly float _speed;
        private readonly float _distance;
    
        public UnitStateFollower(State state, CharacterController characterController, Transform current, Transform target, float speed, float distance) : base(state)
        {
            _characterController = characterController;
            _current = current;
            _target = target;
            _speed = speed;
            _distance = distance * distance;
        }

        public override void Update()
        {
            Vector3 direction = (_target.position - _current.position).normalized;
            direction.y = 0;
            
            _characterController.Move(direction * _speed * Time.deltaTime);
            _current.rotation = Quaternion.LookRotation(direction);
        
            if ((_target.position - _current.position).sqrMagnitude < _distance)
                TransitState();
        }

        protected abstract void TransitState();
    }
}