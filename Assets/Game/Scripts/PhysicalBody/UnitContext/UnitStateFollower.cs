using Game.Scripts.FSM;
using UnityEngine;

namespace Game.Scripts.PhysicalBody.UnitContext
{
    public class UnitStateFollower : StateMachine
    {
        private readonly Transform _current;
        private readonly Transform _target;
        private readonly float _speed;
        private readonly float _distance;
    
        public UnitStateFollower(State state, Transform current, Transform target, float speed, float distance) : base(state)
        {
            _current = current;
            _target = target;
            _speed = speed;
            _distance = distance;
        }

        public override void Update()
        {
            _current.position = Vector3.MoveTowards(_current.position, _target.position, _speed * Time.deltaTime);
            _current.rotation = Quaternion.LookRotation((_target.position - _current.position).normalized);
        
            if ((_target.position - _current.position).sqrMagnitude < _distance)
                State.SetState<UnitStateAttacker>();
        }
    }
}