using System.Collections.Generic;
using System.Linq;

namespace Game.Scripts.PhysicalBody.UnitContext.FSM
{
    public class State
    {
        private StateMachine _currentState;

        private readonly Dictionary<System.Type, StateMachine> _states = new();

        public void Add(StateMachine stateMachine)
        {
            _states.Add(stateMachine.GetType(), stateMachine);
        }

        public void Set<T>() where T : StateMachine
        {
            var requestedType = typeof(T);
            
            var newState = _states.Values.FirstOrDefault(stateMachine => requestedType.IsAssignableFrom(stateMachine.GetType()));

            _currentState?.Exit();
            _currentState = newState;
            _currentState?.Enter();
        }

        public void Update()
        {
            _currentState?.Update();
        }
    }
}
