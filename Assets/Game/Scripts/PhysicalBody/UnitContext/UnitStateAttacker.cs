using Game.Scripts.Damagable;
using Game.Scripts.FSM;
using Game.Scripts.PhysicalBody.UnitContext.Attacker;
using UnityEngine;

namespace Game.Scripts.PhysicalBody.UnitContext
{
    public class UnitStateAttacker : StateMachine
    {
        private readonly IUnitAttacker _attacker;
        private readonly Transform _current;
        private readonly Transform _target;
        private readonly float _distance;
        private readonly int _damage;
        private IDamagable _damagable;
    
        public UnitStateAttacker(State state, IUnitAttacker attacker, Transform current, Transform target, float distance, int damage) : base(state)
        {
            _attacker = attacker;
            _current = current;
            _target = target;
            _distance = distance;
            _damage = damage;
        }

        public override void Enter()
        {
            Ray ray = new Ray(_current.position, _current.forward);

            if (Physics.Raycast(ray, out var hit, _distance))
            {
                if (hit.collider.TryGetComponent(out _damagable))
                {
                    _attacker.Attack(_damagable, _current, _damage);
                }
            }
        }

        public override void Exit()
        {
            Debug.Log("Exit");
        }
    
        public override void Update()
        {
            if ((_target.position - _current.position).sqrMagnitude > _distance)
                State.SetState<UnitStateFollower>();
        }
    }
}